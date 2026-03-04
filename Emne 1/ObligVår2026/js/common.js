function togglePathHint() {
	showPath = !showPath;
	updateView();
}

function createBlankBoard(value = 0) {
	return Array(rowCount * colCount).fill(value);
}

/** @param {number[]} rowAndColumn */
function getCellIndex([row, column]) {
	return row * colCount + column;
}
