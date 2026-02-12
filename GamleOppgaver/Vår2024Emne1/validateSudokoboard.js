/**
 * Check validity of a sudoku grid
 * @param {string} sudoboardString
 * @param {number} size
 * @returns {string} Validity string
 */
function validateSudokuBoard(sudoboardString, size = 4) {
	const individualSquares = sudoboardString.split("");

	if (individualSquares.length !== size ** 2)
		return "ugyldig brett, feil lengde";

	// Regex solution
	// const stringTestRegex = new RegExp(`^[ 1-${size}]+$`);
	// if (!sudoboardString.match(stringTestRegex))
	// 	return "Invalid board: contains invalid characters";

	// Array solution
	const validChars = Array.from({ length: size + 1 }, (_, i) => i.toString());
	validChars[0] = " ";
	if (!individualSquares.every(square => validChars.includes(square)))
		return "ugyldig brett, ugyldig tegn";

	const rows = [];
	for (let i = 0; i < size; i++) {
		let row = "";
		for (let j = 0; j < size; j++) {
			const index = i * size + j;
			row += individualSquares[index];
		}
		rows.push(row);
	}

	const cols = [];
	for (let i = 0; i < size; i++) {
		let col = "";
		for (const row of rows) {
			col += row[i];
		}
		cols.push(col);
	}

	// HARDCODED
	const squares = [
		[rows[0][0], rows[0][1], rows[1][0], rows[1][1]].join(""),
		[rows[0][2], rows[0][3], rows[1][2], rows[1][3]].join(""),
		[rows[2][0], rows[2][1], rows[3][0], rows[3][1]].join(""),
		[rows[2][2], rows[2][3], rows[3][2], rows[3][3]].join(""),
	];
	console.log(squares);

	for (const row of rows) {
		if (containsDuplicateNumber(row)) {
			console.log(row);
			return "Invalid board: duplicate numbers in row";
		}
	}

	for (const col of cols) {
		if (containsDuplicateNumber(col))
			return "Invalid board: duplicate numbers in col";
	}

	return "helt utfylt, ingen feil";
}

/**
 * Checks if a string contains duplicated numbers
 * @param {string} string
 * @param {number} size
 * @returns {boolean} True if string contains duplicate numbers, otherwise false
 */
function containsDuplicateNumber(string) {
	for (let i = 0; i < string.length - 1; i++) {
		for (let j = i + 1; j < string.length; j++) {
			const char1 = string[i];
			const char2 = string[j];
			if (char1 === " " || char2 === " ") continue;
			if (char1 === char2) return true;
		}
	}
	return false;
}
