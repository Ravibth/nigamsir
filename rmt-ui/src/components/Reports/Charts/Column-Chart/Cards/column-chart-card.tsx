import { ColumnChartCardProp, ColumnChartTitle } from "./constant";
import { Grid, Stack, Typography } from "@mui/material";
import { IndianNumberFormatter } from "../../../../../utils/date/dateHelper";
import { Avatar, Card, CardContent } from "@mui/material";
import PersonOutlineOutlinedIcon from "@mui/icons-material/PersonOutlineOutlined";
import { ChartColors } from "../../../constant";

const columnChartTypes = [
  ColumnChartCardProp.TOTAL_CAPACITY,
  ColumnChartCardProp.TOTAL_ALLOCATION,
  ColumnChartCardProp.TOTAL_AVAILABILITY,
  ColumnChartCardProp.TOTAL_ACTUAL,
];
export interface IColumnChartCard {
  [ColumnChartCardProp.TOTAL_CAPACITY]: number;
  [ColumnChartCardProp.TOTAL_ALLOCATION]: number;
  [ColumnChartCardProp.TOTAL_AVAILABILITY]: number;
  [ColumnChartCardProp.TOTAL_ACTUAL]: number;
}

const getIconColor = (keyIndex: number) => {
  switch (keyIndex) {
    case 0:
      return ChartColors.Capacity; // "#00a7b5";
    case 1:
      return ChartColors.Allocation; //"#94CA4E";
    case 2:
      return ChartColors.Availability; //"#4f2d7f";
    default:
      return ChartColors.Actual; //"#00a7b5";
  }
};

const ColumnChartCard = (props: IColumnChartCard) => {
  return (
    <>
      <Stack direction="row" spacing={1} m={2}>
        {columnChartTypes.map((columnChartType, index) => {
          return (
            <Grid item xs={12}>
              <div className="column-chart-capacity-card report-card">
                <Card sx={{ minWidth: 20, maxWidth: 20 }}>
                  <CardContent className="report-card-content">
                    <span>
                      <Typography
                        sx={{ mb: 0, fontSize: 16 }}
                        color="text.secondary"
                      >
                        {ColumnChartTitle[columnChartType]}
                      </Typography>
                      <Typography sx={{ fontSize: 22 }} color="text.primary">
                        {props[columnChartType] >= 1000
                          ? IndianNumberFormatter(props[columnChartType])
                          : props[columnChartType]}
                      </Typography>
                    </span>
                    <Avatar style={{ backgroundColor: getIconColor(index) }}>
                      <PersonOutlineOutlinedIcon />
                    </Avatar>
                  </CardContent>
                </Card>
              </div>
            </Grid>
          );
        })}
      </Stack>
    </>
  );
};

export default ColumnChartCard;
