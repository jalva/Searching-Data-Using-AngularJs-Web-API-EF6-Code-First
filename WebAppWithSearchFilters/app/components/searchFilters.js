app.component('searchFilters', {
    templateUrl: 'views/searchFilters.html',
    bindings: {
        filters: '<',
        onFilterSelected: '&'
    },
    controller: function ($location) {

        var $ctrl = this;
        
        this.$onChanges = function (changes) {

            if (changes.filters && $ctrl.filters) {

                // need to re-select filters based on query string, whenever they get re-rendered
                var qsMap = $location.search();
                for (var i = 0; i < $ctrl.filters.length; i++)
                    $ctrl.filters[i].updateFromQs(qsMap);
            }
        }
    }
});
