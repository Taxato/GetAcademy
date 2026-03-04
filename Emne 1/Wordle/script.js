const CORRECT_WORD = "HELLO";

const letterBtns = document.querySelectorAll(".letter-btn");

for (const btn of letterBtns) {
	btn.addEventListener("click", function (e) {
		const btnValue = e.currentTarget.dataset.btn.toUpperCase();
		switch (btnValue) {
			case "ENTER":
				handleSubmitWord();
				break;
			case "DELETE":
				handleBackSpace();
				break;
			default:
				handleKeyClick(btnValue);
				break;
		}
	});
}

const allGuesses = ["CRANE"];
let currentGuess = [];

function handleSubmitWord() {
	if (currentGuess.length !== 5) return;

	allGuesses.push(currentGuess.join(""));
	currentGuess = [];
	updateView();
}

function handleKeyClick(key) {
	if (currentGuess.length === 5) return;
	currentGuess.push(key);
	updateView();
}

function handleBackSpace() {}

function updateRow(rowIndex, word) {
	const el = document.getElementById(`guess-${rowIndex + 1}`);
	Array.from(el.children).forEach((child, i) => {
		child.innerText = word[i];
	});
}

function updateView() {
	allGuesses.forEach((guess, row) => {
		updateRow(row, guess);
	});
	updateRow(allGuesses.length, currentGuess.join("").padEnd(5, " "));
}

updateView();
