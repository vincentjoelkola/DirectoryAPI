/*$(function () {
    ko.applyBindings(modelView);    
    modelView.viewAssemblies();
 
});

var modelView = {
    Assemblies: ko.observableArray([
        { AssemblyName: "Well-Travelled Kitten", Address1: "Guntur" },
        { AssemblyName: "Speedy Coyote", Address1:"Kurnool" },
      ]),
    IsReceivedAssemblies: ko.observable(false),
    viewAssemblies: function () {
        var thisObj = this;
        console.log(Assemblies);
        try {
            $.ajax({
                url: '/assemblies/',
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    console.log(data);
                    thisObj.Assemblies(data);//Here we are assigning values to KO Observable array
                },
                error: function (err) {
                    alert(err.status + " : " + err.statusText);
                }

            });
        } catch (e) {
            window.location.href = '/Employee/Index';
        }
    },
    gridViewModel: new ko.simpleGrid.viewModel({
        Assemblies: ko.observableArray([
            { AssemblyName: "Well-Travelled Kitten", Address1: "Guntur" },
            { AssemblyName: "Speedy Coyote", Address1: "Kurnool" },
        ]),
        data: Assemblies,
        columns: [
            { headerText: "AssemblyName", rowText: "AssemblyName" },
            { headerText: "Address", rowText: "Address1" },
            //{ headerText: "Price", rowText: function (item) { return "$" + item.price.toFixed(2) } }
        ],
        pageSize: 4
    })
}

function successCallback(data) {
    window.location.href = '/Employee/Index/';
}
function errorCallback(err) {
    window.location.href = '/Employee/Index/';
}*/

var PagedGridModel = function (items) {
    this.items = ko.observableArray(items);

    this.getAssemblies = function () {
        this.items.push({ name: "New item", sales: 0, price: 100 });
    };

    this.addItem = function () {
        this.items.push({ name: "New item", sales: 0, price: 100 });
    };
    this.isAscOrder= false;
    this.sortByName = function () {
        if (this.isAscOrder) {
            this.items.sort(function (a, b) {
                return b.AssemblyName < a.AssemblyName ? -1 : 1;
            });
            this.isAscOrder = false;
        }
        else {
            this.items.sort(function (a, b) {
                return a.AssemblyName < b.AssemblyName ? -1 : 1;
            });
            this.isAscOrder = true;
        }
    };

    this.jumpToFirstPage = function () {
        this.gridViewModel.currentPageIndex(0);
    };

    this.gridViewModel = new ko.simpleGrid.viewModel({
        data: this.items,
        columns: [
            { headerText: "AssemblyName", rowText: "AssemblyName" },
            { headerText: "Address", rowText: "Address1" },
            { headerText: "City", rowText: "City"},
            { headerText: "District", rowText: "District" },
            { headerText: "State", rowText: "State"  },
            { headerText: "Country", rowText: "Country" },
            /*{ headerText: "Edit", rowText: function (item) { return "$" + item.price.toFixed(2) } },*/
            {
                headerText: "Edit", rowText: {
                    action: function (item) {
                        return function () {
                            window.location.href = '/Home/Edit?id=' + item.AssemblyId;
                        }
                    }
                }
            },
            {
                headerText: "Delete", rowText: {
                    action: function (item) {
                        return function () {
                            deleteAssembly(item.AssemblyId);
                        }
                    }
                }
            }
        ],
        pageSize: 50
    });
};

$(function () {

    try {
        $.ajax({
            url: '/assemblies?pagesize=5000',
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json',
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "Authorization": getAccessToken()
            },
            success: function (data) {
                console.log(data);
                ko.applyBindings(PagedGridModel(data.ChristianAssemblies));
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }

        });
    }
    catch (e) {
        window.location.href = '/Employee/Index';
    }


    //$.ajax({
    //    url: 'https://www.universal-tutorial.com/api/getaccesstoken',
    //    type: 'GET',
    //    dataType: 'json',
    //    contentType: 'application/json',
    //    "headers": {
    //        "Accept": "application/json",
    //        "api-token": "e7_LbeHxw7No-gp9ZBP39oZn-fySfIzB-Ra9noxKvLptEktD2hSqhR2iiPx5Vnk6Ngo",
    //        "user-email": "vincent.kola@gmail.com"
    //    },
    //    success: function (data) {
    //        console.log("Universal API");
    //        console.log(data);
    //        var token = "Bearer " + data.auth_token;
    //        $.ajax({
    //            url: 'https://www.universal-tutorial.com/api/countries/',
    //            type: 'GET',
    //            dataType: 'json',
    //            contentType: 'application/json',
    //            "headers": {
    //                "Authorization": token,
    //                "Accept": "application/json"
    //            },
    //            success: function (data) {
    //                console.log("Universal API countries");
    //                console.log(data);

    //            },
    //            error: function (err) {
    //                console.log("Universal API countries");
    //                alert(err.status + " : " + err.statusText);
    //            }

    //        });

    //        $.ajax({
    //            url: 'https://www.universal-tutorial.com/api/states/India',
    //            type: 'GET',
    //            dataType: 'json',
    //            contentType: 'application/json',
    //            "headers": {
    //                "Authorization": token,
    //                "Accept": "application/json"
    //            },
    //            success: function (data) {
    //                console.log("Universal API states");
    //                console.log(data);

    //            },
    //            error: function (err) {
    //                console.log("Universal API states");
    //                alert(err.status + " : " + err.statusText);
    //            }

    //        });

    //        $.ajax({
    //            url: 'https://www.universal-tutorial.com/api/cities/Andhra Pradesh',
    //            type: 'GET',
    //            dataType: 'json',
    //            contentType: 'application/json',
    //            "headers": {
    //                "Authorization": token,
    //                "Accept": "application/json"
    //            },
    //            success: function (data) {
    //                console.log("Universal API cities");
    //                console.log(data);

    //            },
    //            error: function (err) {
    //                console.log("Universal API cities");
    //                alert(err.status + " : " + err.statusText);
    //            }

    //        });
         
    //    },
    //    error: function (err) {
    //        alert(err.status + " : " + err.statusText);
    //    }

    //});
   
});

function deleteAssembly(id) {
    try {
        $.ajax({
            url: '/Assemblies?id=' + id,
            type: 'PUT',
            dataType: 'json',
            contentType: 'application/json',
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "Authorization": getAccessToken()
            },
            success: function (data) {
                alert("Delete Successfully");
                window.location.href = '/Home/Index';
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }

        });
    }
    catch (e) {
        window.location.href = '/Home/Index';
    }
}