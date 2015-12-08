function Card(rank, suit) {
  this.rank = rank;
  this.suit = suit;
}

Card.prototype.value = function() {
  if (typeof this.rank == 'number') {
    return this.rank;
  }

  return this.rank == 'A' ? 11 : 10;
};

function Deck() {
  this.cards = buildDeck();
}

Deck.prototype.shuffle = function() {
  var i = 0,
      j = 0,
      temp = null;

  for (var i = this.cards.length - 1; i > 0; i--) {
    j = Math.floor(Math.random() * (i + 1));
    temp = this.cards[i];
    this.cards[i] = this.cards[j];
    this.cards[j] = temp;
  }
};

Deck.prototype.draw = function() {
  return this.cards.pop();
};

function buildDeck() {
  var suits = ['H', 'C', 'D', 'S'],
      ranks = [2, 3, 4, 5, 6, 7, 8, 9, 10, 'J', 'Q', 'K', 'A'],
      cards = [];

  suits.forEach(function(suit) {
    ranks.forEach(function(rank, index) {
      cards.push(new Card(rank, suit));
    });
  });

  return cards;
}

function Hand(cards) {
  this.cards = cards || [];
}

Hand.prototype.score = function() {
  return this.cards.reduce(function(prev, curr) {
    return prev + curr.value();
  }, 0);
};

Hand.prototype.hit = function(deck) {
  this.cards.push(deck.draw());
};

Hand.prototype.toString = function() {
  return this.cards.reduce(function(result, card) {
    return result += '<div class="card ' + card.suit + '">' + card.rank + '</div>';
  }, '');
};

function Game(deck) {
  this.deck = deck;
  this.deck.shuffle();
  this.dealerPlaying = false;
  this.dealer = new Hand([this.deck.draw(), this.deck.draw()]);
  this.player = new Hand([this.deck.draw(), this.deck.draw()]);
  this.hitButton = document.querySelector('#hit');
  this.stayButton = document.querySelector('#stay');
}

Game.prototype.deal = function() {
  this.printState();
  if (this.player.score() == 21) {
    this.gameOver('You Win!');
  }
};

Game.prototype.printState = function() {
  document.querySelector('.hand').innerHTML = this.player.toString();
  document.querySelector('.score').innerHTML = this.player.score();
  if (this.dealerPlaying) {
    document.querySelector('.dealer').innerHTML = this.dealer.toString();
    document.querySelector('.dealer-score').innerHTML = this.dealer.score();
  }
};

Game.prototype.hit = function() {
  this.player.hit(this.deck);
  this.printState();
  if (this.player.score() > 21) {
    this.gameOver('You Lose');
  } else if (this.player.score() == 21) {
    this.gameOver('You Win!');
  }
};

Game.prototype.stay = function() {
  this.hitButton.disabled = true;
  this.stayButton.disabled = true;
  this.playDealer();
};

Game.prototype.playDealer = function() {
  this.dealerPlaying = true;
  this.printState();
  while (this.dealer.score() < 16) {
    this.dealer.hit(this.deck);
    this.printState();
  }
  if (this.dealer.score() > 21) {
    this.gameOver('You Win!');
  } else if (this.dealer.score() > this.player.score()) {
    this.gameOver('You Lose');
  } else if (this.dealer.score() == this.player.score()) {
    this.gameOver('You Tied');
  } else {
    this.gameOver('You Win!');
  }
};

Game.prototype.gameOver = function(message) {
  this.dealerPlaying = true;
  this.printState();
  alert(message);
  this.hitButton.disabled = true;
  this.stayButton.disabled = true;
};
