function updateView() {
	// Keep search bar active
	const searchInput = document.getElementById("search-input");
	const cursor = searchInput?.selectionStart;
	const hadFocus = document.activeElement === searchInput;

	document.getElementById("app").innerHTML = /*html*/ `
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
	return /*html*/ `
		<h2>Oversikt</h2>
		<div class="filter-group">
			<div class="status-filter-group">
				<button onclick="handleSelectFilter('all')" ${isButtonActive("all")}>Alle</button>
				<button onclick="handleSelectFilter('open')" ${isButtonActive("open")}>Open</button>
				<button onclick="handleSelectFilter('closed')" ${isButtonActive("closed")}>Closed</button>
			</div>
			<button 
				class="priority-sort-button sort-direction-${model.inputs.overview.prioritySortDirection}"
				onclick="handleTogglePrioritySort()">
				<span>Prioritet</span>
				<img src="./public/chevron-right.svg" />
			</button>
		</div>
		${errorsHtml(sortErrorsByPriority(filterErrorsByStatus()))}
	`;
}

function errorsHtml(errors) {
	return errors
		.map(error => {
			const person = model.data.persons.find(p => p.id == error.personId);
			return /*html*/ `
			<div class="error-card">
				<h3>${error.title}</h3>
				<p>Description: ${error.description}</p>
				<p>
					Assigned:
					<b>${person ? person.name : "Ikke satt"}</b>
				</p>
				<p class="severity-${error.severity}">Severity: ${error.severity}</p>
				<p class="priority-${error.priority}">Priority: ${error.priority}</p>
				<p>Status: ${error.status}</p>
				<button onclick="handleDeleteError(${error.id})">Slett</button>
				<button onclick="handleToggleErrorStatus(${error.id})">
					${error.status === "open" ? "Marker som closed" : "Marker som open"}
				</button>
			</div>
		`;
		})
		.join(" ");
}

function createButtonsHtml() {
	return /*html*/ `
	<div class="nav-buttons">
		<button ${isActive("overview")} onclick="selectPage('overview')">Oversikt</button>
		<button ${isActive("search")} onclick="selectPage('search')">Søk</button>
		<button ${isActive("addError")} onclick="selectPage('addError')">Legg til</button>
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
	} else if (model.app.page == "addError") {
		return addErrorHtml();
	}
}

function searchErrorsHtml() {
	let html = /*html*/ `
		<h2>Søk</h2>
		<input
			id="search-input"
			value="${model.inputs.search.searchTerm}"
			oninput="handleSearch(this.value)"
			type="text" 
			placeholder="Søk etter feil..."/>

		${errorsHtml(filterErrorsBySearch())}
	`;

	return html;
}

function addErrorHtml() {
	return /*html*/ `
		<h2>Legg til en feil</h2>
		<form onsubmit="event.preventDefault(); handleAddError()" class="add-error-form">
			<input
				name="title"
				value="${model.inputs.addError.title}"
				onchange="model.inputs.addError.title = this.value"
				required
				type="text"
				placeholder="Tittel" />
			<textarea
				name="description"
				value="${model.inputs.addError.description}"
				onchange="model.inputs.addError.description = this.value"
				required
				style="resize: none"
				placeholder="Beskriv feilen"></textarea>
			<label>Alvorlighetsgrad:</label>
			<select 
				name="severity"
				required
				onchange="model.inputs.addError.severity = this.value">
				<option value="">-- Velg --</option>
				<option value="low" ${model.inputs.addError.severity === "low" ? "selected" : ""}>Low</option>
				<option value="medium" ${model.inputs.addError.severity === "medium" ? "selected" : ""}>Medium</option>
				<option value="high" ${model.inputs.addError.severity === "high" ? "selected" : ""}>High</option>
			</select>
			<label>Prioritet:</label>
			<select 
				name="priority"
				required
				onchange="model.inputs.addError.priority = this.value">
				<option value="">-- Velg --</option>
				<option value="low" ${model.inputs.addError.priority === "low" ? "selected" : ""}>Low</option>
				<option value="medium" ${model.inputs.addError.priority === "medium" ? "selected" : ""}>Medium</option>
				<option value="high" ${model.inputs.addError.priority === "high" ? "selected" : ""}>High</option>
			</select>
			<label>Ansvarlig:</label>
			<select 
				name="personId"
				onchange="model.inputs.addError.personId = this.value">
				<option value="">-- Ikke satt --</option>
				${model.data.persons
					.map(
						p => /*html*/ `
							<option 
								value="${p.id}" 
								${model.inputs.addError.personId == p.id ? "selected" : ""}>
								${p.name}
							</option>`
					)
					.join("")}
			</select>
			<button>Lagre</button>
		</form>
	`;
}

updateView();
