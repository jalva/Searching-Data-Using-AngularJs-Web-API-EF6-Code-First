app.component('searchPagination', {
    templateUrl: 'views/searchPagination.html',
    bindings: {
        totalResultsCount: '<',
        pageSize: '<'
    },
    controller: function ($scope, $rootScope, $location) {

        var $ctrl = this;
        var filterQsKey = 'page';

        this.$onChanges = function (changes) {

            if (changes.totalResultsCount || changes.pageSize) {

                // update pagination (initialize this.pages array)
                rebuildPagination();

                updateFromQs($location.search()[filterQsKey]);
            }
        };

        $scope.$watch(function () { return $location.search()[filterQsKey]; }, function (newValue, oldValue) {
            updateFromQs(newValue);
        });

        $rootScope.$on('filterTermSelected', function (ev, args) {
            // reset to first page for every new search
            selectedPage = null;
            $location.search(filterQsKey, 0);
        });
        
        var selectedPage;

        this.pageClicked = function (page) {
            console.log('page clicked - ' + page.indx);
            $location.search(filterQsKey, page.indx);
        }

        function updateFromQs(pageNum) {
            if (!$ctrl.pages || $ctrl.pages.length == 0)
                return;
            
            if (!isNaN(pageNum))
                pageNum = parseInt(pageNum);
            else
                pageNum = 0;

            var page = $ctrl.pages[pageNum];

            if (selectedPage)
                selectedPage.selected = false;

            if (page) {
                selectedPage = page;
                selectedPage.selected = true;
            }
        }

        function rebuildPagination() {
            var pages = [];
            for (var i = 0; i < $ctrl.totalResultsCount; i += $ctrl.pageSize)
                pages.push({ indx: pages.length });
            $ctrl.pages = pages;
        }
    }
});