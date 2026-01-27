const GAME_LENGTH_MS = 10_000;
const MIN_MOLE_UP_TIME_MS = 200;
const MAX_MOLE_UP_TIME_MS = 2000;

const holes = document.querySelectorAll(".hole");
const scoreBoard = document.querySelector(".score");
const moles = document.querySelectorAll(".mole");

let score = 0;

const moleEventListeners = Array.from({ length: 6 }, () => null);

function randTimeMs(minMs, maxMs) {
	return Math.floor(Math.random() * (maxMs - minMs) + minMs);
}

function updateScoreboard() {
	scoreBoard.innerHTML = score;
}

function hitMole(moleIndex) {
	score++;
	updateScoreboard();
	putDownMole(moleIndex);
}

function putDownMole(moleIndex) {
	const hole = holes[moleIndex];
	hole.classList.remove("up");

	const mole = moles[moleIndex];

	mole.removeEventListener("click", moleEventListeners[moleIndex]);
	moleEventListeners[moleIndex] = null;
}

function popUpRandomMole() {
	const moleIndex = Math.floor(Math.random() * 6);

	const hole = holes[moleIndex];
	if (hole.classList.contains("up")) return;

	setTimeout(
		() => {
			putDownMole(moleIndex);
		},
		randTimeMs(MIN_MOLE_UP_TIME_MS, MAX_MOLE_UP_TIME_MS)
	);

	hole.classList.add("up");
	moleEventListeners[moleIndex] = () => hitMole(moleIndex);

	const mole = moles[moleIndex];
	mole.addEventListener("click", moleEventListeners[moleIndex]);
}

const sleep = ms => new Promise(resolve => setTimeout(resolve, ms));

async function startGame() {
	let gameActive = true;

	setTimeout(() => {
		gameActive = false;
	}, GAME_LENGTH_MS);

	while (gameActive) {
		await sleep(randTimeMs(100, 1000));
		popUpRandomMole();
	}
}
