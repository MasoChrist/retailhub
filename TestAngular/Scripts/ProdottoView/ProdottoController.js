

  prodotti = {

        loadData: function () {
            return this.ListaProdotti;
        },

        insertItem: function (insertingClient) {
        
        },
        updateItem: function () { },
        deleteItem: function (deletingClient) {
           
        },
        DeleteItems: function(prodottiToDelete) {
            for (i = 0; i < prodottiToDelete.length; i++) {
                debugger;
                $.ajax({
                    headers: { 'Token': getToken() },
                    type: "POST",
                    data: JSON.stringify(prodottiToDelete[i]["Identifier"]),
                    url: "../api/DeleteProdottoByID",
                    contentType: "application/json",
                   
                    error: function (xhr, error) {
                        debugger;
                        console.debug(xhr); console.debug(error);
                    }
                });
                var clientIndex = $.inArray(prodottiToDelete[i], this.ListaProdotti);
                this.ListaProdotti.splice(clientIndex, 1);
                
            }
        }
       
    };
    window.Prodotti = prodotti;
    


