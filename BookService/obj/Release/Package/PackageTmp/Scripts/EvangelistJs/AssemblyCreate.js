$(function () {
    ko.applyBindings(modelUpdate);
    modelUpdate.getAvailableServices();
    //modelUpdate.getAssemblyDetails(selectedAssemblyId);
});

var modelUpdate = {
    getAssemblyDetails: function (assemblyId) {
        var thisObj = this;
        try {
            $.ajax({
                url: '/Assemblies?id=' + assemblyId,
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Authorization": getAccessToken()
                },
                success: function (data) {
                    console.log(data);
                    thisObj.AssemblyId(data.AssemblyId);
                    thisObj.AssemblyName(data.AssemblyName);
                    thisObj.WorshipTime(data.WorshipTime);
                    thisObj.NoOfPersons(data.NoOfPersons);
                    thisObj.Address1(data.Address1);
                    thisObj.LandMark(data.LandMark);
                    thisObj.City(data.City);
                    thisObj.District(data.District);
                    thisObj.State(data.State);
                    thisObj.Country(data.Country);
                    thisObj.PinCode(data.PinCode);
                    thisObj.EmailAddress(data.EmailAddress);
                    thisObj.PermanantPhoneNo(data.PermanantPhoneNo);
                    thisObj.ServiceLanguages(data.ServiceLanguages);
                    thisObj.AssemblyElders(data.AssemblyElders);
                },
                error: function (err) {
                    alert(err.status + " : " + err.statusText);
                }

            });
        } catch (e) {
            window.location.href = '/Employee/Index';
        }
    },
    getAvailableServices: function () {
        var thisObj = this;
        try {
            $.ajax({
                url: '/AvailableServices/',
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Authorization": getAccessToken()
                },
                success: function (data) {
                    console.log("Available Services");
                    console.log(data);
                    thisObj.AvailableServices(data);//Here we are assigning values to KO Observable array
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
    AssemblyId: ko.observable(),
    AssemblyName: ko.observable().extend({ required: { message: 'Please enter Assembly Name.' } }),
    WorshipTime: ko.observable(),
    AvailableServices: ko.observableArray(),
    ServiceLanguages: ko.observableArray(),
    NoOfPersons: ko.observable(),
    Address1: ko.observable(),
    LandMark: ko.observable(),
    City: ko.observable(),
    District: ko.observable(),
    State: ko.observable(),
    Country: ko.observable(),
    PinCode: ko.observable(),
    EmailAddress: ko.observable(),
    PermanantPhoneNo: ko.observable(),
    AssemblyElders: ko.observableArray(),

    updateAssembly: function () {

        if (!this.isValid()) {
            this.Errors.showAllMessages();
            return;
        }

        var updateData = {
            AssemblyId: this.AssemblyId,
            AssemblyName: this.AssemblyName,
            WorshipTime: this.WorshipTime,
            ServiceLanguages: ko.toJS(this.ServiceLanguages),
            NoOfPersons: this.NoOfPersons,
            Address1: this.Address1,
            LandMark: this.LandMark,
            City: this.City,
            District: this.District,
            State: this.State,
            Country: this.Country,
            PinCode: this.PinCode,
            EmailAddress: this.EmailAddress,
            PermanantPhoneNo: this.PermanantPhoneNo,
            AssemblyElders: ko.toJS(this.AssemblyElders),
        }; 

        try {
            $.ajax({
                url: '/Assemblies/',
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
            window.location.href = '/Employee/Index/';
        }
    },

    cancel: function () {
        window.location.href = '/Home/index';
    },
    addElder:function ()
    {
        this.AssemblyElders.push(new newElder());
    },

    deleteElder: function () {
        modelUpdate.AssemblyElders.remove(this);
    }
    //End update here   
}

var newElder = function () {
    var self = this;
    self.ElderId = 0;
    self.ElderName = ko.observable();
    self.ElderPhone = ko.observable();
}; 

function successCallback(data) {
    console.log(data);
    window.location.href = '/Home/index';
}
function errorCallback(err) {
    alert(data);
    //window.location.href = '/Home/index1';
}

function cancel() {
    window.location.href = '/Home/index1';
}