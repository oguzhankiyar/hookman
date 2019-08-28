import { Component, OnInit } from '@angular/core';
import { StatService } from '../_services/stat.service';
import { StatTopActionModel } from '../_models/stat.model';
import { BasePagedResponseModel } from '../_models/basePagedResponse.model';
import { LoaderService } from '../_services/loader.service';

declare var $: any;

@Component({
  selector: 'app-dashboard-cmp',
  templateUrl: 'dashboard.component.html'
})

export class DashboardComponent implements OnInit {

  defaultColorScheme = {
    domain: [
      '#d70206',
      '#f05b4f',
      '#f4c63d',
      '#a748ca',
      '#d17905',
      '#59922b',
      '#0544d3',
      '#6b0392',
      '#f05b4f',
      '#dda458',
      '#eacf7d',
      '#86797d',
      '#b2c326',
      '#6188e2'
    ]
  };

  topActionsChart = {
    period: 'this_week',
    view: [1000, 350],
    colorScheme: this.defaultColorScheme,
    showXAxis: true,
    showYAxis: true,
    showLegend: true,
    legendTitle: 'Actions',
    showXAxisLabel: true,
    xAxisLabel: 'Dates',
    showYAxisLabel: true,
    yAxisLabel: 'Hooks',
    timeline: true,
    autoScale: true,
    data: []
  };

  constructor(
    private statService: StatService,
    private loaderService: LoaderService) { }

  ngOnInit(): void {
    this.populateTopActionsChart();
  }

  reloadTopActionsChartData() {
    this.populateTopActionsChart();
  }

  changeTopActionsChartPeriod(period: string) {
    if (this.topActionsChart.period === period) {
      return;
    }

    this.topActionsChart.period = period;
    this.populateTopActionsChart();
  }

  populateTopActionsChart() {
    this.topActionsChart.data = [];

    this.loaderService.show();
    this.statService
      .getTopActionList(this.topActionsChart.period)
      .subscribe((response: BasePagedResponseModel<StatTopActionModel[]>) => {
        this.loaderService.hide();
        this.topActionsChart.data = [];

        for (const model of response.data) {
          const values = [];

          for (const dateValue of model.values) {
            values.push({
              name: dateValue.date,
              value: dateValue.value
            });
          }

          this.topActionsChart.data.push({
            name: model.action,
            series: values
          });
        }
      });
  }
}
