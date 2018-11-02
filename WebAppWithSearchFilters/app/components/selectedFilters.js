app.component('selectedFilters', {
    templateUrl: 'views/searchFilters.html',
    bindings: {
        selectedFilters:'='
    },
    controller: function () {


        this.removeFilterTerm = function (filterTerm) {
            filterTerm.selected = false;

            $ctr.filterSelected($ctr.filter);
        }

        this.showClearAllButton = function () {
            var result = this.selectedFilters.length > 0;
            return result;
        }

        this.clearAllFilters = function () {

        }

    }
});