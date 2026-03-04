function updateView() {
	const boardEl = document.getElementById("board");
	boardEl.style.gridTemplateColumns = `repeat(${colCount}, 40px)`;

	let html = "";
	for (let index = 0; index < map.length; index++) {
		const v = map[index];
		const isFrog = index === frogIndex;
		const isPath = showPath && path.includes(index);

		html += /* html */ `
			<div class="cell ${getCellClass(v)} ${isFrog ? "frog" : ""} ${isPath ? "pathHint" : ""}">
				${getCellIcon(v, isFrog)}
			</div>
    	`;
	}
	boardEl.innerHTML = html;

	document.getElementById("meta").innerHTML = /* html */ `
        <div>
			<b>Frog index:</b> <code>${frogIndex}</code>
		</div>
        <div>
			<b>Path step:</b> <code>${currentStep}</code> / <code>${Math.max(0, path.length - 1)}</code>
		</div>
        <div>
			<b>Path length:</b> <code>${path.length}</code>
		</div>
    `;
}
