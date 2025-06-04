document.getElementById("files").addEventListener("change", e =>{
    e.preventDefault();

    const files = e.target.files;   // получаем все выбранные файлы  
    let output = "";
    for (let i = 0; i < files.length; i++) {        // Перебираем все выбранные файлы   
        const file = files[i];      // Получаем файл 
        console.log(file);
        output += "<li><p><strong>" + file.name + "</strong></p>";
    } 
    document.getElementById("list").innerHTML = "<ul>" + output + "</ul>";
});


