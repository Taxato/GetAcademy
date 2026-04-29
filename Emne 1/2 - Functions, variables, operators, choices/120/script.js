const groceries = ["milk", "bread", "cheese", "eggs"];
let newItem = "";

updateView();
function updateView() {
	document.getElementById("app").innerHTML = /* html */ `
		<ul>
			${showGroceryList(groceries)}
		</ul>

		${showAddItemDiv()}
	`;
}

/**
 * Returns li's of groceries
 * @param {string[]} groceries
 * @returns {string}
 */
function showGroceryList(groceries) {
	let html = "";

	for (let i = 0; i < groceries.length; i++) {
		html += /* html */ `
			<li>
				${groceries[i]}
			</li>
		 `;
	}

	return html;
}

function showAddItemDiv() {
	return /* html */ `
		<div>
			<input type="text" id="grocery-name-input" oninput="newItem = this.value" />
			<button onclick="addItemToList()">Add item to grocery list</button>
		</div>
	`;
}

function addItemToList() {
	if (newItem !== "") groceries.push(newItem);
	updateView();
}
