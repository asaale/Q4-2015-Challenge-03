import random

class Card:
	
	def __init__(self, value, suit):
		self.value = value
		self.suit = suit
	
	def __str__(self):
		return self.value + self.suit

class Deck:
	suits = ['S', 'C', 'H', 'D']
	values = ['2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K', 'A']
	def __init__(self):
		self.cards = []
		self.discard = []
		self.dealer = []
		self.player = []
		for suit in self.suits:
			for value in self.values:
				self.cards.append(Card(value, suit))
		
		self.shuffle()
				
	def shuffle(self):
		for card in self.discard:
			self.cards.append(card)
		del self.discard[:]
		random.shuffle(self.cards)
	
	def discard():
		for card in self.dealer:
			self.discard.append(card)
		del self.dealer[:]
		for card in self.player:
			self.discard.append(card)
		del self.player[:]
		
	def dealCard(self, hand):
		hand.append(self.cards.pop(0))
	
	def score(self, hand):
		score = 0
		
		for card in hand:
			if card.value.isdigit():
				score += int(card.value)
			elif card.value == 'A':
				score += 11
			else:
				score += 10
		if score > 21:
			return 'Bust!'
		return score		

def blackJack():
	deck = Deck()
	deck.dealCard(deck.player)
	deck.dealCard(deck.dealer)
	deck.dealCard(deck.player)
	deck.dealCard(deck.dealer)
	print(handToString(deck.player, 'Player') + " Score: " + str(deck.score(deck.player)))
	if deck.score(deck.player) == 21:
		print('You Win!')
	else:
		choice = input('Would you like to Hit(h) or Stay(s): ')
		while choice.lower() != 's' and deck.score(deck.player) != 'Bust!':
			deck.dealCard(deck.player)
			print(handToString(deck.player, 'Player') + " Score: " + str(deck.score(deck.player)))
			if deck.score(deck.player) == 'Bust!':
				print('You lose.')
				print(handToString(deck.dealer, 'Dealer') + " Score: " + str(deck.score(deck.dealer)))
				break
			choice = input('Would you like to Hit(h) or Stay(s): ')
	
	while deck.score(deck.dealer) != 'Bust!'and deck.score(deck.dealer) < 17:
		deck.dealCard(deck.dealer)
		
	print(handToString(deck.dealer, 'Dealer') + " Score: " + str(deck.score(deck.dealer)))
	
	if deck.score(deck.dealer) == 'Bust!' or deck.score(deck.player) >= deck.score(deck.dealer):
		print('You Win!')
	else:
		print('You Lose.')
	
def handToString(hand, owner):
	out = owner + ': '
	for card in hand:
		out += str(card) + " "
	return out

if __name__ == '__main__':
	blackJack()