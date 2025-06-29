document.addEventListener('DOMContentLoaded', function () {
    // Fonction pour extraire le token de la réponse
    function extractTokenFromResponse(response) {
        try {
            const jsonResponse = JSON.parse(response);
            return jsonResponse.token; // Assurez-vous que le token est dans le champ "token"
        } catch (e) {
            console.error("Erreur lors de l'extraction du token:", e);
            return null;
        }
    }

    // Fonction pour ajouter le bouton "Use Token"
    function addUseTokenButton(token) {
        const authBtnContainer = document.querySelector('.auth-wrapper .btn.authorize');
        if (authBtnContainer) {
            const useTokenBtn = document.createElement('button');
            useTokenBtn.textContent = 'Use Token';
            useTokenBtn.className = 'btn use-token';
            useTokenBtn.style.marginLeft = '10px';
            useTokenBtn.onclick = function () {
                const ui = window.ui;
                if (ui) {
                    ui.preauthorizeApiKey('Bearer', token);
                    alert('Token JWT configuré pour les futures requêtes.');
                }
            };
            authBtnContainer.parentNode.insertBefore(useTokenBtn, authBtnContainer.nextSibling);
        }
    }

    // Intercepter les réponses de l'API
    const originalFetch = window.fetch;
    window.fetch = async function (...args) {
        const response = await originalFetch.apply(this, args);

        // Vérifier si la requête est pour l'API de login
        if (args[0].includes('/api/UserManagement/login') && response.ok) {
            const responseText = await response.clone().text();
            const token = extractTokenFromResponse(responseText);

            if (token) {
                // Ajouter le bouton "Use Token"
                addUseTokenButton(token);
            }
        }

        return response;
    };
});
