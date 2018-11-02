app.component('searchRoot', {
    templateUrl: 'WebAppWithSearchFilters/app/views/searchRoot.html',
    bindings: {
        columns: '<'
    },
    controller: function ($scope, $location, schemaService, searchService, filterService) {

        var $ctrl = this;
        this.pageSize = 5; 
        var initialLoad = true;
        var qs;

        this.selectedFilters = [];
        var selectedFiltersMap = {};


        this.$onInit = function () {

            $scope.$watch(function () { return $location.search(); }, function (newValue, oldValue) {
                console.log('inside $location watcher');
                
                var qs = Object.keys(newValue).map(function (key) {
                    return key + '=' + newValue[key]
                }).join('&');

                doSearch(qs);
                getSearchFilters(qs);


            }, true);

        };


        // *** Get search results
        function doSearch(qs) {
            console.log('inside doSearch() method');
            
            this.loading = true;

            // --- make ajax call ---
            return searchService.getByQs(qs)
                .then(function (pl) {

                    // update results
                    $ctrl.results = pl.data.results;

                    // update query total count
                    $ctrl.totalResultsCount = pl.data.totalCount;
                    

                }, function (errorPl) {
                    this.totalResultsCount = 0;
                    this.addAlert('danger', 'Error requesting the results. ' + myUtils.getExceptionMessage(errorPl));

                });

        }// end doSearch

        
        // *** Get the search filters and their counts
        function getSearchFilters(qs) {
            filterService.getSearchFilters(qs)
                .then(
                    getFiltersCallback,
                    getFiltersErrorCallback
                );
        }

        
        function getFiltersCallback(response) {

            var filters = [];
            for (var i = 0; i < response.data.length; i++) {
                var filter = response.data[i];

                // make sure that all filters and nested filters are of type SearchFilter
                initializeFilterRecursively(filter, filters);
            }

            // initialize the 'filters' collection
            $ctrl.filters = filters;
            
        }

        function getFiltersErrorCallback(error) {
            this.error = error;
            this.loading = false;
        }


        // method initializes filter plain objects (including nested filters) with SearchFilter instances
        function initializeFilterRecursively(filter, filters) {
            if (filter == null)
                return;

            if (filters)
                filters.push(new SearchFilter(filter));
            
            for (var i = 0; i < filter.filterTerms.length; i++) {
                var filterTerm = filter.filterTerms[i];
                if (filterTerm.nestedFilters) {
                    for (var i2 = 0; i2 < filterTerm.nestedFilters.length; i2++) {
                        var nestedFilter = filterTerm.nestedFilters[i2];
                        initializeFilterRecursively(nestedFilter, null);

                        filterTerm.nestedFilters[i2] = new SearchFilter(nestedFilter); 
                    }
                }
            }
        }

        // on filter selected callback
        $scope.$on('filterTermSelected', function (ev, args) {
            console.log('root onFilterSelected');
            var filter = args[0];
            var map = filter.getFlattenedSelectionsMap();
            for (var filterKey in map)
                $location.search(filterKey, map[filterKey]);
        });
        
    }
});