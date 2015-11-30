package game;

import java.util.ArrayList;
import java.util.List;

public abstract class Person {

	private List<Card> hand = new ArrayList<Card>();
	
	public void hit(List<Card> cards) {
		hand.add(cards.remove(0));
	}
	
	public int calcValue() {
		int value = 0;
		for (Card card : hand) {
			value += card.getValue().getValue();
		}
		
		return value;
	}
	
	public void deal(List<Card> cards) {
		hit(cards);
	}
}
