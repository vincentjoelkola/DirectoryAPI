$(function () {
    ko.applyBindings(modelUpdate);
    modelUpdate.getEvangelistDetails(requestId);
    modelUpdate.viewAssemblies();
});

$(document).ready(function () {
    $("#DOB").datepicker(
        {
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd/mm/yy',
            yearRange: '-100:+0"',
            onSelect: function (selected, evnt) {
                $('#DOB').val(selected);
                modelUpdate.DOB(selected);
            }
        }
    );

    $("#DateOfCommMinistry").datepicker(
        {
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd/mm/yy',
            yearRange: '-100:+0"',
            onSelect: function (selected, evnt) {
                $('#DateOfCommMinistry').val(selected);
                modelUpdate.DateOfCommMinistry(selected);
            }
        }
    );
});

function registerDatePicker() {
    /*$("#datetimepicker1").datepicker(
        {
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd/mm/yy',
            yearRange: '-100:+0"',
            onSelect: function (selected, evnt) {
                $('#datetimepicker1').val(selected);
                modelUpdate.DOB(selected);
            }
        }
    );*/

   
   
}


ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var $el = $(element);

        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {};
        $el.datepicker(options);


        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            observable($el.datepicker("getDate"));
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $el.datepicker("destroy");
        });

    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            $el = $(element),
            current = $el.datepicker("getDate");

        if (value - current !== 0) {
            $el.datepicker("setDate", value);
        }
    }
};

var modelUpdate = {
    AssemblyFilterText: ko.observable(""),
    Assemblies: ko.observableArray([]),
    viewAssemblies: function () {
        var thisObj = this;
        try {
            $.ajax({
                url: '/assemblies/',
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Authorization": getAccessToken()
                },
                success: function (data) {
                    thisObj.Assemblies(data.ChristianAssemblies);//Here we are assigning values to KO Observable array
                },
                error: function (err) {
                    alert(err.status + " : " + err.statusText);
                }

            });
        } catch (e) {
            window.location.href = '/Employee/Index';
        }
    },
    getEvangelistDetails: function (selectedEvangelistId) {
        var thisObj = this;
        try {
            $.ajax({
                url: '/EvangelistRequest?id=' + selectedEvangelistId,
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Authorization": getAccessToken()
                },
                success: function (data) {
                    console.log(data);
                    thisObj.EvangelistId(data.EvangelistId);
                    thisObj.Name(data.Name);
                    //thisObj.PhotoUrl("/eimages/"+data.PhotoUrl);
                    thisObj.PhotoUrl(data.PhotoUrl);
                    thisObj.Status(data.Status);
                    thisObj.DOB(data.DOB);
                    thisObj.EduQualification(data.EduQualification);
                    thisObj.WifesName(data.WifesName);
                    thisObj.WifesAge(data.WifesAge);
                    thisObj.NoofChildren(data.NoofChildren);
                    thisObj.Children(data.Children);
                    thisObj.DateOfCommMinistry(data.DateOfCommMinistry);
                    thisObj.Address1(data.Address1);
                    thisObj.LandMark(data.LandMark);
                    thisObj.City(data.City);
                    thisObj.District(data.District);
                    thisObj.State(data.State);
                    thisObj.Country(data.Country);
                    thisObj.PinCode(data.PinCode);
                    thisObj.EmailAddress(data.EmailAddress);
                    thisObj.PermanantPhoneNo(data.PermanantPhoneNo);
                    thisObj.WhatsAppNo(data.WhatsAppNo);
                    thisObj.Assembly(data.Assembly);
                    thisObj.CommdAssembly(data.CommdAssembly);
                },
                error: function (err) {
                    alert(err.status + " : " + err.statusText);
                }

            });
        } catch (e) {
            window.location.href = '/Employee/Index';
        }
    },
    //Update
    EvangelistId: ko.observable(),
    Name: ko.observable(),
    PhotoUrl: ko.observable(),
    getPhotoUrl: function () {
        return "/eimages/" + this.PhotoUrl();
    },
    Status: ko.observable(),
    DOB: ko.observable(),
    EduQualification: ko.observable(),
    WifesName: ko.observable(),
    WifesAge: ko.observable(),
    NoofChildren: ko.observable(),
    Children: ko.observableArray(),
    DateOfCommMinistry: ko.observable(),
    Address1: ko.observable(),
    LandMark: ko.observable(),
    City: ko.observable(),
    District: ko.observable(),
    State: ko.observable(),
    Country: ko.observable(),
    PinCode: ko.observable(),
    EmailAddress: ko.observable(),
    PermanantPhoneNo: ko.observable(),
    WhatsAppNo: ko.observableArray(),
    Assembly: ko.observable(),
    CommdAssembly: ko.observable(null),
    updateEvangelist: function () {
        console.log(ko.toJSON(this));

        var updateEvgData = {
            EvangelistId: this.EvangelistId,
            Name: this.Name, 
            DOB: this.DOB,
            EduQualification: this.EduQualification,
            WifesName: this.WifesName,
            WifesAge: this.WifesAge,
            NoofChildren: this.NoofChildren,
            Children: ko.toJS(this.Children),
            DateOfCommMinistry: this.DateOfCommMinistry,
            Address1: this.Address1,
            LandMark: this.LandMark,
            City: this.City,
            District: this.District,
            State: this.State,
            Country: this.Country,
            PinCode: this.PinCode,
            EmailAddress: this.EmailAddress,
            PermanantPhoneNo: this.PermanantPhoneNo,
            WhatsAppNo: this.WhatsAppNo,
            Assembly: ko.toJS(this.Assembly),
            CommdAssembly: ko.toJS(this.CommdAssembly),
        }; 


        try {
            $.ajax({
                url: '/Evangelists/',
                type: 'POST',
                dataType: 'json',
                data: updateEvgData, //ko.toJSON(this),
                contentType: 'application/json',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Authorization": getAccessToken()
                },
                success: successCallback,
                error: errorCallback
            });
        } catch (e) {
            window.location.href = '/Employee/Index/';
        }
    },

    approveRequest: function () {
        
        var updateData = {
            RequestId: requestId,
            EvangelistId: 0,
            Name: this.Name,
            PhotoUrl: this.PhotoUrl,
            DOB: this.DOB,
            EduQualification: this.EduQualification,
            WifesName: this.WifesName,
            WifesAge: this.WifesAge,
            NoofChildren: this.NoofChildren,
            Children: ko.toJS(this.Children),
            DateOfCommMinistry: this.DateOfCommMinistry,
            Address1: this.Address1,
            LandMark: this.LandMark,
            City: this.City,
            District: this.District,
            State: this.State,
            Country: this.Country,
            PinCode: this.PinCode,
            EmailAddress: this.EmailAddress,
            PermanantPhoneNo: this.PermanantPhoneNo,
            WhatsAppNo: this.WhatsAppNo,
            Assembly: ko.toJS(this.Assembly),
            CommdAssembly: ko.toJS(this.CommdAssembly),
        };

        try {
            $.ajax({
                url: '/SaveEditedEvangelistRequest/',
                type: 'POST',
                dataType: 'json',
                data: updateData,
                contentType: 'application/json',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Authorization": getAccessToken()
                },
                success: successCallback,
                error: errorCallback
            });
        } catch (e) {
            alert(e);
        }
    },

    rejectRequest: function () {

        try {
            $.ajax({
                url: '/RejectEvangelistRequest?id=' + requestId,
                type: 'GET',
                contentType: 'application/json',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Authorization": getAccessToken()
                },
                success: successrejectCallback,
                error: errorCallback
            });
        } catch (e) {
            console.log(e);
            alert(e);
        }
    },

    cancel: function () {
        window.location.href = '/EvangelistRequest/index';
    },
    addChild:function ()
    {
        this.Children.push(new newElder());
    },

    deleteChild: function () {
        modelUpdate.Children.remove(this);
    },

    showAssemblies: function (vm) {
        this.selectedAssembly(vm);
        $('#myModal').modal('show');
    },

    isOpen: ko.observable(false),
    selectedItem: ko.observable(),
    open: function () {
        this.isOpen(true);
        //this.selectedItem("Test");
    },
    close: function () {
        this.isOpen(false);
    },

    searchAssemblies: function () {
        var query = this.AssemblyFilterText().toLowerCase();
        if (!query) {
            return this.Assemblies();
        } else {
            var AssembliesList = this.Assemblies();
            return AssembliesList.filter(function (assembly) {
                let assemblyName = assembly.AssemblyName ? assembly.AssemblyName.toLowerCase() : "";
                let address = assembly.Address1 ?assembly.Address1.toLowerCase(): "";
                let city = assembly.City ? assembly.City.toLowerCase() : "";
                let district = assembly.District ? assembly.District.toLowerCase() : "";
                let landmark = assembly.LandMark ? assembly.LandMark.toLowerCase() : "";
                let state = assembly.State ? assembly.State.toLowerCase() : "";
                let country = assembly.Country ? assembly.Country.toLowerCase() : "";
                return (assemblyName.indexOf(query) > -1 || 
                    address.indexOf(query) > -1 ||
                    city.indexOf(query) > -1 ||
                    district.indexOf(query) > -1 ||
                    landmark.indexOf(query) > -1 ||
                    state.indexOf(query) > -1 ||
                    country.indexOf(query) > -1)
            });
        }
    },

    gridViewModel: function () {
        var self = this;
        return new ko.simpleGrid.viewModel({
            data: ko.observableArray(this.modelUpdate.searchAssemblies()),
            columns: [
                { headerText: "AssemblyName", rowText: "AssemblyName" },
                { headerText: "Address", rowText: "Address1" },
                { headerText: "City", rowText: "City" },
                { headerText: "District", rowText: "District" },
                { headerText: "State", rowText: "State" },
                { headerText: "Country", rowText: "Country" },
                {
                    headerText: "Select", rowText: {
                        action: function (item) {
                            return function () {
                                //self.modelUpdate.Assembly(item);
                                //self.modelUpdate.close();
                                self.selectAssembly(item);
                            }
                        }
                    }
                }
            ],
            pageSize: 50
        });
    }

   
};

var newElder = function () {
    var self = this;
    self.ChildId = 0;
    self.ChildName = ko.observable();
    self.Age = ko.observable();
}; 

function successCallback(data) {
    alert("Evangelist details updated");
    window.location.href = '/EvangelistRequests/index';
}

function errorCallback(err) {
    alert(err);
    //window.location.href = '/Home/index1';
}

function cancel() {
    window.location.href = '/EvangelistRequests/index';
}

function showAssemblies() {
    this.selectedAssembly(vm);
    $('#myModal').modal('show');
}

var blnselectAssembly = false;
var blnselectcomdAssembly = false;
function openDialog() {
    blnselectAssembly = true;
    blnselectcomdAssembly = false;
    $("#dialog").dialog();
    $("#dialog").dialog("option", "width", 1000);
    $("#dialog").dialog("option", "height", 500);
}

function openCommdDialog() {
    blnselectAssembly = false;
    blnselectcomdAssembly = true;
    $("#dialog").dialog();
    $("#dialog").dialog("option", "width", 1000);
    $("#dialog").dialog("option", "height", 500);
}

function selectAssembly(assembly) {
    if (blnselectAssembly)
        modelUpdate.Assembly(assembly);
    else if (blnselectcomdAssembly)
        modelUpdate.CommdAssembly(assembly);
    $("#dialog").dialog("close");
    
}
