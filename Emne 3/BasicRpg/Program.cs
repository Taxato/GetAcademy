// See https://aka.ms/new-console-template for more information

using BasicRpg;

Game game = new();
Character hero = new("PC", "Link", 50, 3);
Goblin goblin = new();

game.Fight(hero, goblin);