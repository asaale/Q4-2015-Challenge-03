package com.stgutah.playground.games.bj;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Scanner;
import java.util.logging.Logger;

/**
 * My Black Jack Game.
 * <p/>
 * Created by dqromney on 12/1/15.
 */
public class MyBlackJack {

    private static Logger log = Logger.getLogger(MyBlackJack.class.getName());

    private Scanner scanner = new Scanner(System.in);
    private Deck cardDeck;
    private Hand dealer;
    private Hand user;
    private Boolean playAgain = Boolean.FALSE;

    private void execute() {
        do {
            showBanner();
            playAgain = doBlackJack();
        } while (playAgain);
        System.out.println("Thank you for playing!");
    }

    private Boolean doBlackJack() {
        cardDeck.shuffle();
        // New Game
        dealer.getHand().clear();
        user.getHand().clear();

        // Deal the first two cards
        dealer.addCard(cardDeck.dealCard());
        dealer.addCard(cardDeck.dealCard());
        user.addCard(cardDeck.dealCard());
        user.addCard(cardDeck.dealCard());

        if (value(dealer.getHand()) == 21) {
            System.out.println("Dealer has " + dealer.toString() + ".");
            System.out.println("User hand has " + user.toString() + ".");
            System.out.println();
            System.out.println("Dealer has Blackjack!  Dealer wins.");
            return playAgain();
        }

        if (value(user.getHand()) == 21) {
            System.out.println("Dealer has " + dealer.toString() + ".");
            System.out.println("User hand has " + user.toString() + ".");
            System.out.println();
            System.out.println("You have Blackjack!  You win.");
            return playAgain();
        }

        while (true) {

            // Display the user's cards and begin play.
            System.out.println();
            System.out.println();
            System.out.println("Your cards are:");
            System.out.println(user.showHand());
            System.out.println("Your total is " + value(user.getHand()));
            System.out.println();
            System.out.println("Dealer is showing the \n" + dealer.getHand().get(0).renderCard());
            System.out.println();
            System.out.print("Select 'H'it  or 'S'tand? ");

            // User's response, 'H' or 'S'.
            char userAction;
            do {
                userAction = Character.toUpperCase(scanner.next().charAt(0));
                if (userAction != 'H' && userAction != 'S') {
                    System.out.print("Please respond H or S: ");
                }
            } while (userAction != 'H' && userAction != 'S');

            // if the user selects 'H'it, deal the user a card, and if user selects 'S'tand,
            // then the dealer get a chance to draw and end the game.
            if (userAction == 'S') {
                // Loop ends; user is done taking cards.
                break;
            } else {
                // Give the user a card.  If the user goes over 21, the user loses.
                Card newCard = cardDeck.dealCard();
                user.addCard(newCard);
                System.out.println();
                System.out.println("User requests a hit.");
                System.out.println("Your card is the \n" + newCard.renderCard());
                System.out.println("Your total is now " + value(user.getHand()));
                if (value(user.getHand()) > 21) {
                    System.out.println();
                    System.out.println("You busted by going over 21.  You lose.");
                    System.out.println("Dealer's other card was the \n" + dealer.getHand().get(1).renderCard());
                    return playAgain();
                }
            }
        } // end while loop

        // The stands with 21 or less. Dealer draws until total is greater-than 16.
        System.out.println();
        System.out.println("User stands.");
        System.out.println("Dealer's cards are");
        System.out.println(dealer.showHand());
        while (value(dealer.getHand()) <= 16) {
            Card newCard = cardDeck.dealCard();
            System.out.println("Dealer hits and gets the \n" + newCard.renderCard());
            dealer.addCard(newCard);
        }
        System.out.println("Dealer's total is " + value(dealer.getHand()));

        // Declare the winner!
        System.out.println();
        if (value(dealer.getHand()) > 21) {
            System.out.println("Dealer hand is a bust by going over 21.  You win!");
            return playAgain();
        } else {
            if (value(dealer.getHand()) == value(user.getHand())) {
                System.out.println("Tie game! You win on a tie!");
                return playAgain();
            } else {
                if (value(dealer.getHand()) > value(user.getHand())) {
                    System.out.println("Dealer wins, " + value(dealer.getHand()) + " points to " + value(user.getHand()) + ".");
                    return playAgain();
                } else {
                    System.out.println("You win, " + value(user.getHand()) + " points to " + value(dealer.getHand()) + ".");
                    return playAgain();
                }
            }
        }
    }

    private void init(String[] args) {
        cardDeck = new Deck();
        dealer = new Hand();
        user = new Hand();
    }

    private Boolean playAgain() {
        Boolean playAgain = Boolean.FALSE;
        char userAction;
        System.out.print("\nWould you like to play again (Y/N)? ");
        do {
            userAction = Character.toUpperCase(scanner.next().charAt(0));
            if (userAction != 'Y' && userAction != 'N') {
                System.out.print("Please respond 'Y' or 'N':  ");
            }
        } while (userAction != 'Y' && userAction != 'N');
        if (userAction == 'Y') {
            playAgain = Boolean.TRUE;
        }
        return playAgain;
    }

    /**
     * Returns the value of this hand for the game of Blackjack.
     *
     * @param hand a list of {@link Card} objects
     * @return the value of this hand
     */
    public Integer value(List<Card> hand) {
        // The value computed for the hand.
        Integer handValue;
        // This will be set to true if the hand contains an ace.
        boolean ace;
        // Number of cards in the hand.
        int cards;

        handValue = 0;
        ace = false;
        cards = hand.size();

        for (int i = 0; i < cards; i++) {
            // Add the value of the i-th card in the hand.
            Card card = hand.get(i); // The i-th card;
            int cardValue = card.getFaceValue();  // The normal value, 1 to 13.
            if (cardValue == 1) {
                // There is at least one ace.
                ace = true;
            }
            handValue = handValue + cardValue;
        }

        // Handle the Ace card, if changing it's value from 1 to 11 would cause the score less than or
        // equal to 21, then add 10 points to value.
        if (ace == true && handValue + 10 <= 21) {
            handValue = handValue + 10;
        }

        return handValue;
    }

    private void showBanner() {
        // clearScreen();
        String title =
                "██████╗ ██╗      █████╗  ██████╗██╗  ██╗     ██╗ █████╗  ██████╗██╗  ██╗\n" +
                "██╔══██╗██║     ██╔══██╗██╔════╝██║ ██╔╝     ██║██╔══██╗██╔════╝██║ ██╔╝\n" +
                "██████╔╝██║     ███████║██║     █████╔╝      ██║███████║██║     █████╔╝ \n" +
                "██╔══██╗██║     ██╔══██║██║     ██╔═██╗ ██   ██║██╔══██║██║     ██╔═██╗ \n" +
                "██████╔╝███████╗██║  ██║╚██████╗██║  ██╗╚█████╔╝██║  ██║╚██████╗██║  ██╗\n" +
                "╚═════╝ ╚══════╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝ ╚════╝ ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝\n";
        System.out.print(title);
    }

    private static void clearScreen() {
        for (int a = 0; a < 100; a++) System.out.print("\n");
    }

    /**
     * Main driver.
     *
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        MyBlackJack main = new MyBlackJack();
        main.init(args);
        main.execute();
    }

    // ----------------------------------------------------------------
    // Internal classes
    // ----------------------------------------------------------------

    /**
     * Deck class
     */
    class Deck {

        private Integer DECK_COUNT = 52;
        private Card[] deck;
        private Integer position;

        public Deck() {
            // Create an un-shuffled deck of cards.
            deck = new Card[DECK_COUNT];
            int cardCount = 0;
            for (int suit = 0; suit <= 3; suit++) {
                for (Integer value = 1; value <= 13; value++) {
                    deck[cardCount] = new Card(value, Suit.findSuitByValue(suit));
                    cardCount++;
                }
            }
        }

        public void shuffle() {
            // Put all the used cards back into the deck, and shuffle it into
            // a random order.
            System.out.print("Shuffling deck ");
            for (int i = (DECK_COUNT - 1); i > 0; i--) {
                int rand = (int) (Math.random() * (i + 1));
                Card temp = deck[i];
                deck[i] = deck[rand];
                deck[rand] = temp;
                System.out.print(".");
            }
            System.out.println();
            position = 0;
        }

        public Card dealCard() {
            // Deals one card from the deck and returns it.
            if (position == 52) {
                shuffle();
            }
            position++;
            return deck[position - 1];
        }

        public Card[] getDeck() {
            return deck;
        }

        public void setDeck(Card[] deck) {
            this.deck = deck;
        }

        public Integer getPosition() {
            return position;
        }

        public void setPosition(Integer position) {
            this.position = position;
        }

        @Override
        public String toString() {
            final StringBuilder sb = new StringBuilder("Deck{");
            sb.append("deck=").append(Arrays.toString(deck));
            sb.append(", position=").append(position);
            sb.append('}');
            return sb.toString();
        }
    }

    /**
     * Hand class.
     */
    class Hand {
        private List<Card> hand;

        public Hand() {
            hand = new ArrayList<Card>();
        }

        public void addCard(Card card) {
            hand.add(card);
        }

        public String showHand() {
            String[][] hand = buildHand();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Card.CARD_LINES; i++) {
                for (int j = 0; j < hand.length; j++) {
                    if (hand[j][i] == null) {
                        break;
                    }
                    sb.append(hand[j][i]);
                }
                sb.append("\n");
            }
            return sb.toString();
        }

        private String[][] buildHand() {
            String[][] cards = new String[10][7];
            int i = 0;
            for (Card card : hand) {
                cards[i++] = card.getImage();
            }
            return cards;
        }

        public List<Card> getHand() {
            return hand;
        }

        public void setHand(List<Card> hand) {
            this.hand = hand;
        }

    }

    /**
     * Card class.
     */
    static class Card {
        private Integer value;
        private Suit suit;
        private Integer faceValue;
        public static Integer CARD_LINES = 7;
        public static Integer FACE_CARD_VALUE = 10;

        public Card(Integer value, Suit suit) {
            this.value = this.faceValue = value;
            this.suit = suit;
            if (value > FACE_CARD_VALUE) {
                faceValue = FACE_CARD_VALUE;
            }
        }

        public Integer getValue() {
            return value;
        }

        public void setValue(Integer value) {
            this.value = value;
        }

        public Suit getSuit() {
            return suit;
        }

        public void setSuit(Suit suit) {
            this.suit = suit;
        }

        public Integer getFaceValue() {
            return faceValue;
        }

        public void setFaceValue(Integer faceValue) {
            this.faceValue = faceValue;
        }

        public String renderCard() {
            StringBuilder sb = new StringBuilder();
            Character type = getCardType();
            sb.append(String.format("╔═══════╗\n"))
                .append(String.format("║ %c     ║\n", type))
                .append(String.format("║       ║\n"))
                .append(String.format("%s\n", this.suit.getName()))
                .append(String.format("║       ║\n"))
                .append(String.format("║     %c ║\n", type))
                .append(String.format("╚═══════╝\n"));
            return sb.toString();
        }

        public String[] getImage() {
            Character type = getCardType();
            String[] card = {
                    String.format("╔═══════╗"),
                    String.format("║ %c     ║", type),
                    String.format("║       ║"),
                    String.format("%s", this.suit.getName()),
                    String.format("║       ║"),
                    String.format("║     %c ║", type),
                    String.format("╚═══════╝")
            };
            return card;
        }

        private Character getCardType() {
            return this.value == 1 ? 'A' :
                    this.value == 11 ? 'J' :
                            this.value == 12 ? 'Q' :
                                    this.value == 13 ? 'K' :
                                            this.value.toString().charAt(0);
        }

        @Override
        public String toString() {
            final StringBuilder sb = new StringBuilder("Card{");
            sb.append("value=").append(value);
            sb.append(", suit=").append(suit).append(suit.getSuitChar());
            sb.append(", faceValue=").append(faceValue);
            sb.append('}');
            return sb.toString();
        }
    }

    /**
     * Suit enumerator.
     */
    enum Suit {
        SPADE(0, "║ Spade ║", 'S'),
        CLUB(1, "║ Club  ║", 'C'),
        DIAMOND(2, "║Diamond║", 'D'),
        HEART(3, "║ Heart ║", 'H'),
        UNDEFINED(-1, "Undefined", ' ');

        private Integer suit;
        private String name;
        private char suitChar;

        Suit(Integer suit, String name, int suitChar) {
            this.suit = suit;
            this.name = name;
            this.suitChar = (char) suitChar;
        }

        public static Suit findSuitByValue(Integer value) {
            Suit result = Suit.UNDEFINED;
            for (Suit suit : Suit.values()) {
                if (value == suit.getSuit()) {
                    result = suit;
                    break;
                }
            }
            return result;
        }

        public Integer getSuit() {
            return suit;
        }

        public String getName() {
            return name;
        }

        public char getSuitChar() {
            return suitChar;
        }
    }
}
