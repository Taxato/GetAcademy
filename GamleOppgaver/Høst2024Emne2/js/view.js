function updateView() {
	document.getElementById("app").innerHTML = /*HTML*/ `
        <h1>Filer og mapper</h1>
        Du er her: ${createBreadCrums()}<br/>
		${createCreationButtonsHtml()}
        ${createFoldersHtml()}
        ${createFilesHtml()}
        ${createEditFileHtml()}
    `;
}

function createBreadCrums() {
	let curId = model.app.currentId;
	if (curId == null) return /* html */ `<i>rotmappe</i>`;

	let breadcrumbs = "";
	while (curId) {
		let fileOrFolder = model.filesAndFolders.find(f => f.id == curId);
		if (breadcrumbs != "") breadcrumbs = " > " + breadcrumbs;
		breadcrumbs = fileOrFolder.name + breadcrumbs;
		curId = fileOrFolder.parentId;
	}
	return breadcrumbs;
}

function createCreationButtonsHtml() {
	const parentId = model.app.currentId;
	return /* html */ `
		<div>
			<input type="text" id="element-name-input" placeholder="Navn på ny mappe/fil" />
			<button onclick="createFolder(${parentId},document.getElementById('element-name-input').value)">Opprett mappe</button>
			<button onclick="createFile(${parentId},document.getElementById('element-name-input').value)">Opprett fil</button>
		</div>
	`;
}

function createFoldersHtml() {
	let currentId = model.app.currentId;
	const currentFileOrFolder = model.filesAndFolders.find(
		f => f.id == currentId
	);
	let html = "";
	if (currentFileOrFolder != null) {
		html = /* html */ `<a href="javascript:select(${currentFileOrFolder.parentId})">📁 ..</a><br/>`;
		if (currentFileOrFolder.hasOwnProperty("content"))
			currentId = currentFileOrFolder.parentId;
	}
	for (let folder of model.filesAndFolders) {
		if (folder.hasOwnProperty("content") || folder.parentId != currentId)
			continue;
		html += /* html */ `
			<div>
				<a href="javascript:deleteElement(${folder.id})">❌</a>
				<a href="javascript:select(${folder.id})">📁 ${folder.name}</a>
			</div>`;
	}
	return html;
}

function createFilesHtml() {
	let currentId = model.app.currentId;
	const currentFileOrFolder = model.filesAndFolders.find(
		f => f.id == currentId
	);
	if (currentFileOrFolder != null) {
		if (currentFileOrFolder.hasOwnProperty("content"))
			currentId = currentFileOrFolder.parentId;
	}
	let html = "";
	for (let file of model.filesAndFolders) {
		if (!file.hasOwnProperty("content") || file.parentId != currentId)
			continue;
		html += /* html */ `
			<div>
				<a href="javascript:deleteElement(${file.id})">❌</a>
				<a href="javascript:select(${file.id})"><span>🗎</span> ${file.name}</a><br/>
			</div>
			`;
	}
	return html;
}

function createEditFileHtml() {
	const currentId = model.app.currentId;
	if (currentId == null) return "";
	const currentFile = model.filesAndFolders.find(f => f.id == currentId);
	if (!currentFile.hasOwnProperty("content")) return "";
	return /*HTML*/ `
        <textarea id="file-edit-textarea">${currentFile.content}</textarea>    
        <br/>
        <button onclick="updateFile(${currentId},document.getElementById('file-edit-textarea').value)">Lagre</button>
        <button onclick="updateView()">Avbryt</button>
    `;
}
