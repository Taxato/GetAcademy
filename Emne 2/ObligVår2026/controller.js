function selectPage(page) {
	model.app.page = page;
	updateView();
}

function handleSelectFilter(filter) {
	model.inputs.overview.activeFilter = filter;

	updateView();
}

function handleSearch(searchTerm) {
	model.inputs.search.searchTerm = searchTerm;

	updateView();
}

function filterErrorsBySearch() {
	const searchTerm = model.inputs.search.searchTerm.toLowerCase();
	return model.data.errors.filter(
		error =>
			error.title.toLowerCase().includes(searchTerm) ||
			error.description.toLowerCase().includes(searchTerm)
	);
}

function filterErrorsByStatus() {
	return model.data.errors.filter(
		error =>
			model.inputs.overview.activeFilter === "all" ||
			error.status === model.inputs.overview.activeFilter
	);
}

function sortErrorsByPriority(errors) {
	const priorityToNumber = pri => ["low", "medium", "high"].indexOf(pri);
	return [...errors].sort((a, b) => {
		const aPri = priorityToNumber(a.priority);
		const bPri = priorityToNumber(b.priority);
		return model.inputs.overview.prioritySortDirection === "ascending"
			? aPri - bPri
			: bPri - aPri;
	});
}

function handleTogglePrioritySort() {
	model.inputs.overview.prioritySortDirection =
		model.inputs.overview.prioritySortDirection === "ascending"
			? "descending"
			: "ascending";
	updateView();
}

function handleAddError() {
	const modelData = model.inputs.addError;
	model.data.latestErrorId += 1;
	const newError = {
		id: model.data.latestErrorId,
		title: modelData.title,
		description: modelData.description,
		severity: modelData.severity,
		priority: modelData.priority,
		status: "open",
		personId: Number(modelData.personId) || null,
	};
	model.data.errors.push(newError);

	// Clear inputs in model
	model.inputs.addError = {
		title: "",
		description: "",
		severity: "",
		priority: "",
		personId: null,
	};

	selectPage("overview");
}

function handleDeleteError(errorId) {
	const error = model.data.errors.find(err => err.id === errorId);

	// Don't delete if error bug is status "open"
	if (error.status === "open") return;

	model.data.errors.splice(model.data.errors.indexOf(error), 1);

	updateView();
}

function handleToggleErrorStatus(errorId) {
	const error = model.data.errors.find(err => err.id === errorId);
	error.status = error.status === "open" ? "closed" : "open";

	updateView();
}
