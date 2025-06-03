// NOTE : перенес скрипты в отдельный файл

async function CategoryData() {
    let token = localStorage.getItem("access_token");

    try {
        let response = await fetch("http://localhost:5232/api/Categories", {
            headers: {
                "Content-Type": "application/json",
                Authorization: "Bearer " + token,
            },
        });

        if (!response.ok) {
            throw new Error(`Ошибка: ${response.status}`);
        }

        let data = await response.json();
        console.log(data);

        const select = document.getElementById("category");
        data.forEach((category) => {
            const option = document.createElement("option");
            option.value = category.id; // UUID из Supabase
            option.textContent = category.name; // Название категории
            select.appendChild(option);
        });
    } catch (err) {
        console.error("Ошибка при получении категорий:", err);
    }
}

CategoryData();

async function CuisineData() {
    let token = localStorage.getItem("access_token");

    try {
        let response = await fetch(
            "http://localhost:5232/api/CategoriesDishCuisine",
            {
                headers: {
                    "Content-Type": "application/json",
                    Authorization: "Bearer " + token,
                },
            }
        );

        if (!response.ok) {
            throw new Error(`Ошибка: ${response.status}`);
        }

        let data = await response.json();
        console.log(data);

        const select = document.getElementById("cuisine");
        data.forEach((cuisine) => {
            const option = document.createElement("option");
            option.value = cuisine.id; // UUID из Supabase
            option.textContent = cuisine.name; // Название категории
            select.appendChild(option);
        });
    } catch (err) {
        console.error("Ошибка при получении видов кухонь:", err);
    }
}

CuisineData();

async function MenuData() {
    let token = localStorage.getItem("access_token");

    try {
        let response = await fetch("http://localhost:5232/api/CategoriesMenu", {
            headers: {
                "Content-Type": "application/json",
                Authorization: "Bearer " + token,
            },
        });

        if (!response.ok) {
            throw new Error(`Ошибка: ${response.status}`);
        }

        let data = await response.json();
        console.log(data);

        const select = document.getElementById("menu");
        data.forEach((menu) => {
            const option = document.createElement("option");
            option.value = menu.id; // UUID из Supabase
            option.textContent = menu.name; // Название категории
            select.appendChild(option);
        });
    } catch (err) {
        console.error("Ошибка при получении категорий:", err);
    }
}

MenuData();


// обработка и отправка данных после нажатия на кнопку 
document.getElementById("saveButton").addEventListener("click", async (e) => {
    e.preventDefault();

    // создание данных для отправки 
    // названия полей должны соответствовать наименованию класса указанного requets, иначе [frombody] работать не будет!!!! 

    const recipeData = { // аналог new Model = { } в c#
        // получаем поля 
        Name: document.getElementById("name").value,
        ShortDescription: document.getElementById("short_description").value,
        CatKey: document.getElementById("category").value,
        CatCuisineKey: document.getElementById("cuisine").value,
        CatMenuKey: document.getElementById("menu").value,
        Squirrels: document.getElementById("squirells").value,
        Fats: document.getElementById("fats").value,
        Carbohydrates: document.getElementById("carbohydrates").value,
        Steps: document.getElementById("steps").value,
    };

    console.log(recipeData); // вывод в консоль для отладки 

    let token = localStorage.getItem("access_token"); // получение токена

    // отправляем нашу модель recipeData на бекенд 
    let response = await fetch('http://localhost:5232/api/SaveRecipe/createrecipe', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            Authorization: "Bearer " + token, // обязательно указываем auth тк контроллер требует [Authorize]
        },
        body: JSON.stringify(recipeData) // в теле запроса указываем нашу модель для отправки
    })


    // хоть как-то обрабатываем ответ 
    if (response.ok) {
        console.log('успешный успех')
    } else {
        console.log('неудача')
    }

});
