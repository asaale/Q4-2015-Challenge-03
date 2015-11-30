#Challenge #3 (due 12/7/2015): Blackjack Poker Game
Let’s kick off this week’s code challenge by implementing a simple game of blackjack.  This is a little more ambitious than the first two challenges; however, for those feeling really, really ambitious, an extra entry is awarded for also implementing a simple poker game.

##Description
Implement a simplified, 2-player version of the popular blackjack card game, with you versus the computer.  To keep things simple, here are the rules we will follow in our version:
* The game begins with a standard deck of 52 cards (no jokers), randomly shuffled.
* You need to include some sort of a Shuffle()method that can shuffle any remaining cards not in play
* To keep things simple, no betting or gambling will be involved in this version
The object of the game is to get as close to 21 (a “blackjack”) without going over.  Cards 2 through 10 are worth their face value, Aces are always worth 11 (usually it can be worth 1 or 11, but let’s not complicate this too much), Jacks, Queens, and Kings are worth 10 points each.
* You begin with 2 cards that are immediately revealed to you.  If they add up to 21, you automatically win regardless of what the computer has.
* After the 2 cards, you have the option to “hit” (deal another card), or “stand” (be done) as long as your total score is under 21.  If at any time you go over 21, you lose, regardless of what the computer has.
* Once you choose to stand, the computer’s score is revealed (computer plays with the remaining cards after your turn).  In terms of programming the A.I. for this, feel free to keep it as simple as possible.  One option is to always have a pre-set threshold after which the computer will always stand regardless.  To keep things simple, there are only going to be two turns total: 1) you continually play until you either choose to stand or go over 21, then 2) the computer proceeds to do the same thing, after which you will find out the computer’s score.
* In the event of a tie, you automatically win.

##Bonus Entry
Blackjack is getting pretty boring.  Let’s also give the option to play a basic 2-player poker game of you vs. the computer.  Here are the rules:
* The game begins with a standard deck of 52 cards (no jokers), randomly shuffled
* To keep things simple, no betting, gambling, or folding will be involved in this version
* You need to include some sort of a Shuffle()method that can shuffle any remaining cards not in play
* The object of the game is to get a higher hand than your opponent; here are the hands, ranked from highest to lowest: http://www.pokersyte.com/basic/hand-rankings-mobile.pdf
* You both are dealt 5 cards.  Unlike regular poker, to keep this version simple: 
1) there will be no round of trading in cards.  A winner is immediately determined after the cards are dealt. 
2) allow a tie if both of you have the same hand ranking (i.e. a pair of Kings and a pair of Aces would result in a tie)
