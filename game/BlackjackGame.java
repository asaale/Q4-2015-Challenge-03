package game;

import java.util.Collections;
import java.util.List;

public class BlackjackGame {

	public static void main(String[] args) {
		Game game = new Game();
		
		List<Card> cards = game.buildDeck();
		
		//dealers usually shuffle cards 3 times
		
		Collections.shuffle(cards);
		Collections.shuffle(cards);
		Collections.shuffle(cards);
		
		game.deal(cards);
		
		game.play(cards);
	}
}
