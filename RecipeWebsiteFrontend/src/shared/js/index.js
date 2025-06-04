document.addEventListener('DOMContentLoaded', async () => {
    try {
        const response = await fetch('http://localhost:5232/api/Recipes');
        
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        const recipes = await response.json();
        console.log('Received data:', recipes); // Проверьте структуру данных
        
        if (!Array.isArray(recipes)) {
            throw new Error('Expected array but got ' + typeof recipes);
        }
        
        renderRecipes(recipes);
    } catch (error) {
        console.error('Fetch error:', error);
        document.getElementById('recipesContainer').innerHTML = `
            <div class="error">Ошибка загрузки: ${error.message}</div>
        `;
    }
});


function renderRecipes(recipes) {
    const container = document.getElementById('recipesContainer');
    
    if (recipes.length === 0) {
        container.innerHTML = `
            <div class="col-12">
                <div class="alert alert-info">
                    Рецептов пока нет. Будьте первым, кто добавит рецепт!
                </div>
            </div>
        `;
        return;
    }
    
    container.innerHTML = recipes.map(recipe => `
        <div class="col-md-4 col-sm-6">
            <a href="/src/pages/recipe/Recipe.html?id=${recipe.id}" class="text-decoration-none">
                <div class="recipe-card">
                    
                    <div class="recipe-info">
                        <h3 class="recipe-title">${recipe.name}</h3>
                        <div class="recipe-time">

                            <!-- // !!! ТУТ ВЫВОДИТСЯ КАРТИНКА ИЗ СУПАБЕЙЗА НАДО НАСТРОИТЬ СТИЛИ -->
                            <img src="${recipe.imgUrl}" alt="" style="width: 50%">

                            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                            </svg>
                            ${recipe.cookingTime || 'Время не указано'}
                        </div>
                    </div>
                </div>
            </a>
        </div>
    `).join('');
}