var ViewModel = function () {
    var self = this;
    self.books = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.authors = ko.observableArray();
    self.assemblies = ko.observableArray();
    self.assemblydetail = ko.observable();
    self.availableServices = ko.observableArray(["Telugu", "English"]);
    //self.newBook = ko.observable();
    self.newBook = {
        AssemblyId: ko.observable(),
        AssemblyName: ko.observable(),
        NoOfPersons: ko.observable(),
        WorshipTime: ko.observable(),
        Address1: ko.observable(),
        LandMark: ko.observable(),
        City: ko.observable(),
        District: ko.observable(),
        State: ko.observable(),
        Country: ko.observable(),
        PinCode: ko.observable(),
        PermanantPhoneNo: ko.observable(),
        EmailAddress: ko.observable(),
        availableServices: ko.observableArray(["Telugu", "English", "Tamil", "Kannada", "Hindi"]),
        chosenServices: ko.observableArray(["Telugu", "English"]),
        churchElders: ko.observableArray(),
    }

    function ChurchElder(id,name, phone) {
        this.AssemblyElderId = id;
        this.ElderName = name;
        this.ElderPhone = phone;
    }

    var booksUri = '/api/books/';
    var authorsUri = '/api/authors/';
    var assemblyUri = '/assemblies/';
    var assemblyDetailsUri = '/Assemblies?id=';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllBooks() {
        ajaxHelper(booksUri, 'GET').done(function (data) {
            self.books(data);
        });
    }

    self.getBookDetail = function (item) {
        ajaxHelper(booksUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    self.getAssemblyDetail = function (item) {
        ajaxHelper(assemblyDetailsUri + item.AssemblyId, 'GET').done(function (data) {
            self.detail(data);
            self.newBook = data;
            console.log("getAssemblyDetail");
            console.log(data);
        });
    }

    function getAuthors() {
        ajaxHelper(authorsUri, 'GET').done(function (data) {
            self.authors(data);
        });
    }

    function getAssemblies() {
        ajaxHelper(assemblyUri, 'GET').done(function (data) {
            console.log("getAssemblies");
            console.log(data);
            self.assemblies(data);
        });
    }


    self.addBook = function (formElement) {
        var book = {
            AuthorId: self.newBook.Author().Id,
            Genre: self.newBook.Genre(),
            Price: self.newBook.Price(),
            Title: self.newBook.Title(),
            Year: self.newBook.Year()
        };

        ajaxHelper(booksUri, 'POST', book).done(function (item) {
            self.books.push(item);
        });
    }

    // Fetch the initial data.
    getAssemblies();
    //getAllBooks();
    //getAuthors();
};

ko.applyBindings(new ViewModel());