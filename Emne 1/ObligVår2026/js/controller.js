/** @param {number} type */
function getCellClass(type) {
	if (type === WATER) return "water";
	if (type === ROCK) return "rock";
	if (type === GOAL) return "goal";
	return "safe";
}

/**
 * @param {number} type
 * @param {boolean} isFrog
 */
function getCellIcon(type, isFrog) {
	if (isFrog) return "🐸";
	if (type === WATER) return "🌊";
	if (type === ROCK) return "🪨";
	if (type === GOAL) return "🏁";
	return "";
}

/**
 * Sets global "path" variable to number[] or returns null if failed to generate path
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

	// Convert list of coordinates to list of indeces and set path to new list
	path = newPath.map(getCellIndex);
}

/**
 * @param {number[]} path
 */
function randomizeBoard(path) {
	for (let i = 0; i < map.length; i++) {
		const rand = Math.random();
		if (rand > 0.75)
			map[i] = WATER; // 25% chance
		else if (rand > 0.5) map[i] = ROCK; // 25% chance
	}

	// Clear every map position that is on the path
	for (const pos of path) {
		map[pos] = SAFE;
	}

	// Set the goal
	map[path.at(-1)] = GOAL;
}

function resetGame() {
	map.fill(0);
	path = [];
	while (path.length === 0) generatePath(20);
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
