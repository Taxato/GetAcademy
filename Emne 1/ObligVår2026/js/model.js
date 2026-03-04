const rowCount = 8;
const colCount = 10;

let map = createBlankBoard();
let frogIndex = null;

let path = [];
let currentStep = 0;
let showPath = false;

// Direction constants
const LEFT = 0;
const UP_LEFT = 1;
const UP = 2;
const UP_RIGHT = 3;
const RIGHT = 4;

// Cell type constants
const SAFE = 0;
const WATER = 1;
const ROCK = 2;
const GOAL = 3;
