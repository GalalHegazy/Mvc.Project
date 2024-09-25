
/// Search 
const SearchBar = $('#SearchInput');
const table = $('.Emp-Card');
SearchBar.on('keyup', function (event) {

    var searchValue = SearchBar.val();

    $.ajax({
        url: '/Employee/Search',
        type: 'Get',
        data: { SearchInput: searchValue },
        success: function (result) {
            table.html(result)
        },
        error: function (xhr, status, error) {
            console.log(error)
        }
    })
});

const SearchBarDep = $('#SearchInputDep');
const tableDep = $('.Dep-Card');
SearchBarDep.on('keyup', function (event) {

    var searchValue = SearchBarDep.val();

    $.ajax({
        url: '/Department/Search',
        type: 'Get',
        data: { SearchInputDep: searchValue },
        success: function (result) {
            tableDep.html(result)
        },
        error: function (xhr, status, error) {
            console.log(error)
        }
    })
});
const SearchBarUser = $('#SearchInputUser');
const tableUser = $('.User-Card');
SearchBarDep.on('keyup', function (event) {

    var searchValue = SearchBarUser.val();

    $.ajax({
        url: '/Users/Index',
        type: 'Get',
        data: { SearchInputUser: searchValue },
        success: function (result) {
            tableUser.html(result)
        },
        error: function (xhr, status, error) {
            console.log(error)
        }
    })
});
