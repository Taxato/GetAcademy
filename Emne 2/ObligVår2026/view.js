function updateView() {
	// Keep search bar active
	const searchInput = document.getElementById("search-input");
	const cursor = searchInput?.selectionStart;
	const hadFocus = document.activeElement === searchInput;

	document.getElementById("app").innerHTML = /*HTML*/ `
        <h1>BugTracker</h1>
        ${createButtonsHtml()}
        ${renderPage()}
    `;

	// Re-Set focus
	if (hadFocus) {
		const newInput = document.getElementById("search-input");
		newInput.focus();
		newInput.setSelectionRange(cursor, cursor);
	}
}

function overviewHtml() {
	return /*HTML*/ `
		<h2>Oversikt</h2>
		<div class="filter-group">
			<button onclick="handleSelectFilter('all')" ${isButtonActive("all")}>Alle</button>
			<button onclick="handleSelectFilter('open')" ${isButtonActive("open")}>Open</button>
			<button onclick="handleSelectFilter('closed')" ${isButtonActive("closed")}>Closed</button>
		</div>
		${errorsHtml(model.data.errors)}
	`;
}

function errorsHtml(errors) {
	return errors
		.map(error => {
			const person = model.data.persons.find(p => p.id == error.personId);
			return /*HTML*/ `
			<div class="error-card">
				<h3>${error.title}</h3>
				<p>Description: ${error.description}</p>
				<p>
					Assigned:
					<b>${person ? person.name : "Ikke satt"}</b>
				</p>
				<p class="severity-${error.severity}">Severity: ${error.severity}</p>
				<p>Status: ${error.status}</p>
				<button onclick="deleteError()">Slett</button>
				<button onclick="toggleStatus()">
					${error.status === "open" ? "Merk som closed" : "Merk som open"}
				</button>
			</div>
		`;
		})
		.join(" ");
}

function createButtonsHtml() {
	return /*HTML*/ `
	<div class="nav-buttons">
		<button ${isActive("overview")} onclick="selectPage('overview')">Oversikt</button>
		<button ${isActive("search")} onclick="selectPage('search')">Søk</button>
		<button ${isActive("addErrors")} onclick="selectPage('addErrors')">Legg til</button>
	</div>
	`;
}

function isActive(currentPage) {
	return model.app.page == currentPage ? "class='active'" : "";
}

function isButtonActive(btn) {
	return model.inputs.overview.activeFilter === btn ? 'class="active"' : "";
}

function renderPage() {
	if (model.app.page == "overview") {
		return overviewHtml();
	} else if (model.app.page == "search") {
		return searchErrorsHtml();
	} else if (model.app.page == "addErrors") {
		return addErrorsHtml();
	}
}

function searchErrorsHtml() {
	let html = /*HTML*/ `
		<h2>Søk</h2>
		<input
			id="search-input"
			value="${model.inputs.search.searchTerm}"
			oninput="handleSearch(this.value)"
			type="text" 
			placeholder="Søk etter feil..."/>

		${errorsHtml(filterErrors())}
	`;

	return html;
}

function addErrorsHtml() {
	return /*HTML*/ `
		<h2>Legg til en feil</h2>
		<div class="add-error-form">
			<input
				type="text"
				placeholder="Tittel" />
			<textarea
				style="resize: none"
				placeholder="Beskriv feilen"></textarea>
			<label>Alvorlighetsgrad:</label>
			<select>
				<option value="">-- Velg --</option>
				<option value="low">Low</option>
				<option value="medium">Medium</option>
				<option value="high">High</option>
			</select>
			<label>Ansvarlig:</label>
			<select>
				<option value="">-- Ikke satt --</option>
				${model.data.persons
					.map(
						p => `
				<option value="${p.id}">${p.name}</option>
				`
					)
					.join("")}
			</select>
			<button>Lagre</button>
		</div>
	`;
}

updateView();
