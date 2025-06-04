async function renderRecipe() {
    let token = localStorage.getItem("access_token");

    // const payload = JSON.parse(atob(token.split('.')[1]));

    // const userId = payload.sub;

    // console.log(userId);

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



    ingredients.innerHTML = "";
    if (response.ok) {
        let data = await response.json();
        console.log(data);
        name.innerHTML = data.name;
        steps.innerHTML = data.steps;
        data.ingredients.forEach((ingredient) => {
            console.log(ingredient.productName);
            ingredients.innerHTML += `
                <div class="ingredient-item">
                    ${ingredient.productName}
                </div>
                `;
        });
        Calories.innerHTML = data.calories;
        CookingTime.innerHTML = data.cookingTime;
        Fats.innerHTML = data.fats;
        Proteins.innerHTML = data.proteins;
        ShortDescription.innerHTML = data.shortDescription;
        Carbohydrates.innerHTML = data.carbohydrates;
    }
}

renderRecipe();
