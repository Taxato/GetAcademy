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

function filterErrors() {
	const searchTerm = model.inputs.search.searchTerm.toLowerCase();
	return model.data.errors.filter(
		error =>
			error.title.toLowerCase().includes(searchTerm) ||
			error.description.toLowerCase().includes(searchTerm)
	);
}
