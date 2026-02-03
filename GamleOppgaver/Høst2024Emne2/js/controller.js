function select(id) {
	model.app.currentId = id;
	updateView();
}

function updateFile(fileId, newContent) {
	const file = model.filesAndFolders.find(f => f.id === fileId);
	if (file) file.content = newContent;
}

function createFolder(parentFolderId, newFolderName) {
	createItem(parentFolderId, newFolderName);
}

function createFile(parentFolderId, newFileName) {
	createItem(parentFolderId, newFileName, true);
}

function createItem(parentFolderId, newItemName, isFile = false) {
	const newId = Math.max(...model.filesAndFolders.map(f => f.id)) + 1;
	const newElement = {
		id: newId,
		name: newItemName,
	};
	if (parentFolderId !== null) newElement.parentId = parentFolderId;
	if (isFile) newElement.content = "";

	console.log("Created new element:", newElement);

	model.filesAndFolders.push(newElement);

	updateView();
}

function deleteElement(elementId) {
	// We need to traverse the model to delete all children and grandchildren etc..
	const parentIdQueue = [elementId];

	while (parentIdQueue.length) {
		const parentId = parentIdQueue.pop();

		// Get all children
		const children = model.filesAndFolders.filter(
			f => f.parentId === parentId
		);

		for (const child of children)
			// Add child id to parent id queue to delete its children etc..
			parentIdQueue.push(child.id);

		// Delete parent from model
		model.filesAndFolders.splice(
			model.filesAndFolders.findIndex(f => f.id === parentId),
			1
		);
	}

	updateView();
}
