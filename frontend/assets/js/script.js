
try {
    document.getElementById('year-tag').innerHTML = new Date().getFullYear();
} catch(error) {
    console.error(`year tag failure!`);
}