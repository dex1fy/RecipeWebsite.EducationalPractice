async function renderRecipe() {
    let token = localStorage.getItem("access_token");


    let params = new URLSearchParams(window.location.search);

    // Получаем значение параметра "id"
    let recipeId = params.get("id");

    console.log(recipeId);
    let response = await fetch(`http://localhost:5232/api/Recipe/${recipeId}`, {
        headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + token,
        },
    });

    let name = document.getElementById("name");
    let ingredients = document.getElementById("ingredients");
    let steps = document.getElementById("steps");
    let CookingTime = document.getElementById("CookingTime");
    let Calories = document.getElementById("Calories");
    let Fats = document.getElementById("Fats");
    let Proteins = document.getElementById("Proteins");
    let ShortDescription = document.getElementById("ShortDescription");
    let Carbohydrates = document.getElementById("Carbohydrates");



    // ingredients.innerHTML = "";
    if (response.ok) {
        let data = await response.json();
        
        // Основная информация
        name.innerHTML = data.name;
        ShortDescription.innerHTML = data.shortDescription;
        
        // Форматирование времени
        CookingTime.innerHTML = data.cookingTime ? `${data.cookingTime}` : '—';
        
        // Пищевая ценность
        Calories.innerHTML = data.calories ? `${data.calories} ккал` : '—';
        Proteins.innerHTML = data.proteins ? `${data.proteins} г` : '—';
        Fats.innerHTML = data.fats ? `${data.fats} г` : '—';
        Carbohydrates.innerHTML = data.carbohydrates ? `${data.carbohydrates} г` : '—';
        
        // Ингредиенты и шаги
        ingredients.innerHTML = data.ingredients.map(ing => 
            `<div class="ingredient-item">${ing.productName}</div>`
        ).join('');
        
        steps.innerHTML = data.steps.replace(/\n/g, '<br>');
    }
}

renderRecipe();
