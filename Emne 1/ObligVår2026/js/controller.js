/** @param {number} v */
function getCellClass(v) {
	if (v === WATER) return "water";
	if (v === ROCK) return "rock";
	if (v === GOAL) return "goal";
	return "safe";
}

/**
 * @param {number} v
 * @param {boolean} isFrog
 */
function getCellIcon(v, isFrog) {
	if (isFrog) return "🐸";
	if (v === WATER) return "🌊";
	if (v === ROCK) return "🪨";
	if (v === GOAL) return "🏁";
	return "";
}

/**
 * Sets global "path" variable to number[] or null if failed to generate path
 * @param {number} maxSteps
 */
function generatePath(maxSteps) {
	// Initial coordinates is random cell on bottom row
	let currentCoords = [rowCount - 1, Math.floor(Math.random() * colCount)];
	const newPath = [currentCoords];

	let step = 0;
	while (step < maxSteps) {
		if (currentCoords[0] === 0) {
			console.log("Reached top");
			break;
		}

		const rand = Math.random();

		let stepDirection = UP;

		if (rand > 0.9)
			// 10% chance
			stepDirection = LEFT;
		else if (rand > 0.75)
			// 15% chance
			stepDirection = UP_LEFT;
		else if (rand > 0.6)
			// 15% chance
			stepDirection = UP_RIGHT;
		else if (rand > 0.5)
			// 10% chance
			stepDirection = RIGHT;

		// Remaining chance for stepDirection to be UP - 50%

		const nextCoords = [...currentCoords];

		if (
			stepDirection === UP ||
			stepDirection === UP_LEFT ||
			stepDirection === UP_RIGHT
		)
			nextCoords[0] -= 1; // Move up

		if (stepDirection === LEFT || stepDirection === UP_LEFT) {
			if (nextCoords[1] === 0) {
				console.log("Hit left edge");
				return null;
			}
			nextCoords[1] -= 1; // Move left
		}

		if (stepDirection === RIGHT || stepDirection === UP_RIGHT) {
			if (nextCoords[1] === colCount - 1) {
				console.log("Hit right edge");
				return null;
			}
			nextCoords[1] += 1; // Move right
		}

		newPath.push(nextCoords);
		currentCoords = nextCoords;
		step++;
	}

	path = newPath.map(getCellIndex);
}

/**
 * @param {number[]} path
 */
function randomizeBoard(path) {
	for (let i = 0; i < map.length; i++) {
		const rand = Math.random();
		if (rand > 0.75) map[i] = WATER;
		else if (rand > 0.5) map[i] = ROCK;
	}

	for (const pos of path) {
		map[pos] = 0;
	}

	map[path.at(-1)] = 3;
}

function newBoard() {
	map.fill(0);
	path = null;
	while (path === null || path.length === 0) generatePath(20);
	currentStep = 0;
	frogIndex = path[currentStep];
	randomizeBoard(path);
	updateView();
}

function prevStep() {
	if (currentStep === 0) return;
	currentStep--;
	frogIndex = path[currentStep];
	updateView();
}
function nextStep() {
	if (currentStep === path.length - 1) return;
	currentStep++;
	frogIndex = path[currentStep];
	updateView();
}
