package game;

import java.util.ArrayList;
import java.util.List;

public class Game {
	
	private Player player;
	private Dealer dealer;

	public Game() {
		player = new Player();
		dealer = new Dealer();
	}
	
	public List<Card> buildDeck() {
		
		List<Card> cards = new ArrayList<Card>();
		
		for (Suit suit : Suit.values()) {
			for (Value value : Value.values()) {
				cards.add(new Card(suit, value));
			}
		}
		
		return cards;
		
	}
	
	public void deal(List<Card> cards) {
		//deal one to the player then the dealer
		
		player.deal(cards);
		dealer.deal(cards);
		player.deal(cards);
		dealer.deal(cards);
	}

	public void play(List<Card> cards) {
		play(player, cards, 18); 
		
		if(player.calcValue() > 21) {
			System.out.print("Player bust!!! Dealer wins!!!");
		}
		else {
			play(dealer, cards, 17);
		
			if(dealer.calcValue() > 21) {
				System.out.print("Dealer bust!!! Player wins!!!");
			}
			else {
				if (dealer.calcValue() > player.calcValue()) {
					System.out.println("Dealer wins!!!");
				}
				else {
					System.out.println("Player wins!!!");
				}
			}
		}
	}
	
	private void play(Person person, List<Card> cards, int threshold) {
		while(person.calcValue() < threshold) {
			person.hit(cards);
		}
	}
}
