

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
                webApiCall("POST", "DeleteProdottoByID", prodottiToDelete[i]["Identifier"]).then(
                    function () {
                        var clientIndex = $.inArray(prodottiToDelete[i], this.ListaProdotti);
                        this.ListaProdotti.splice(clientIndex, 1);
                    });
                
            }
        },
        GetProdottoByDTOSearch:function(description) {
           
            debugger;
            var searcher = {
                PartialDescription: description
            };
            debugger;
            webApiCall("POST", "GetProdottoByDTOSearch", searcher).then(function (newData) {
                debugger;
                LoadGrid2(newData);
            });
}
       
    };
    window.Prodotti = prodotti;
    


