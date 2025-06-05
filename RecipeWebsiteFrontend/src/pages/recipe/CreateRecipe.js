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
            "http://localhost:5232/api/CategoriesDishCuisine/Rewew",
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

async function ProductData(selectElement = null) {
    let token = localStorage.getItem("access_token");

    try {
        let response = await fetch("http://localhost:5232/api/Ingredients", {
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

        // Если передан конкретный select - заполняем только его
        if (selectElement) {
            fillSelectWithProducts(selectElement, data);
        } 
        // Иначе заполняем все существующие select'ы
        else {
            document.querySelectorAll('select[name="product[]"]').forEach(select => {
                fillSelectWithProducts(select, data);
            });
        }
    } catch (err) {
        console.error("Ошибка при получении продуктов:", err);
    }
}

// Вспомогательная функция для заполнения select'а
function fillSelectWithProducts(select, products) {
    // Очищаем select перед заполнением (кроме первого option, если он есть)
    const firstOption = select.querySelector('option[value=""]');
    select.innerHTML = firstOption ? firstOption.outerHTML : '';
    
    products.forEach((product) => {
        const option = document.createElement("option");
        option.value = product.id;
        option.textContent = product.name;
        select.appendChild(option);
    });
}
ProductData();



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

let productsCache = null; // Кэш для хранения списка продуктов

async function loadProducts() {
    let token = localStorage.getItem("access_token");

    try {
        let response = await fetch("http://localhost:5232/api/Ingredients", {
            headers: {
                "Content-Type": "application/json",
                Authorization: "Bearer " + token,
            },
        });

        if (!response.ok) throw new Error(`Ошибка: ${response.status}`);
        
        productsCache = await response.json();
        console.log("Продукты загружены:", productsCache);
        
        // Заполняем все существующие select'ы
        fillAllProductSelects();
        
    } catch (err) {
        console.error("Ошибка при получении продуктов:", err);
    }
}

// Заполняет конкретный select продуктами
function fillProductSelect(selectElement) {
    if (!productsCache) return;
    
    // Сохраняем выбранное значение (если есть)
    const selectedValue = selectElement.value;
    
    // Очищаем select (оставляя первый пустой option если есть)
    const firstOption = selectElement.querySelector('option[value=""]');
    selectElement.innerHTML = firstOption ? firstOption.outerHTML : '';
    
    // Заполняем options
    productsCache.forEach(product => {
        const option = document.createElement("option");
        option.value = product.id;
        option.textContent = product.name;
        selectElement.appendChild(option);
    });
    
    // Восстанавливаем выбранное значение (если оно есть в новых options)
    if (selectedValue && selectElement.querySelector(`option[value="${selectedValue}"]`)) {
        selectElement.value = selectedValue;
    }
}

// Заполняет все select'ы на странице
function fillAllProductSelects() {
    document.querySelectorAll('select[name="product[]"]').forEach(select => {
        fillProductSelect(select);
    });
}

// Функции для работы с ингредиентами
function addIngredientField() {
    const container = document.getElementById('ingredientsContainer');
    const newRow = document.createElement('div');
    newRow.className = 'ingredient-row';
    newRow.innerHTML = `
        <select name="product[]" class="form-control"></select>
        <button type="button" class="ingredient-btn add-btn">
            <i>+</i>
        </button>
        <button type="button" class="ingredient-btn remove-btn">
            <i>×</i>
        </button>
    `;
    container.appendChild(newRow);
    
    // Заполняем новый select продуктами
    fillProductSelect(newRow.querySelector('select'));
    updateRemoveButtons();
}

function removeIngredientField(row) {
    row.remove();
    updateRemoveButtons();
}

function updateRemoveButtons() {
    const rows = document.querySelectorAll('.ingredient-row');
    rows.forEach((row, index) => {
        const removeBtn = row.querySelector('.remove-btn');
        removeBtn.style.display = rows.length > 1 ? 'flex' : 'none';
    });
}

// Инициализация при загрузке страницы
document.addEventListener('DOMContentLoaded', function() {
    // Загружаем продукты
    loadProducts();
    
    // Инициализируем кнопки
    updateRemoveButtons();
    
    // Обработчики событий
    const container = document.getElementById('ingredientsContainer');
    container.addEventListener('click', function(e) {
        if (e.target.classList.contains('add-btn') || e.target.closest('.add-btn')) {
            addIngredientField();
        }
        
        if (e.target.classList.contains('remove-btn') || e.target.closest('.remove-btn')) {
            removeIngredientField(e.target.closest('.ingredient-row'));
        }
    });
});

