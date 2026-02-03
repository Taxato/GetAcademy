const letterBtns = document.querySelectorAll(".letter-btn");

for (const btn of letterBtns) {
	btn.addEventListener("click", function (e) {
		const btnValue = e.currentTarget.dataset.btn;
		console.log(btnValue);
	});
}

const currentGuess = Array.from({ length: 5 }, () => null);
