/* eslint-disable react-hooks/exhaustive-deps */
import { useEffect, memo } from "react";
import {
  IPropsCardView,
  IScoreBreakup,
  ISystemSuggestions,
} from "./interfaces";
import {
  Box,
  Card,
  CardContent,
  Checkbox,
  Grid,
  Typography,
} from "@mui/material";
import { UserDetailsHeaderSxProps, DataGridSxProps } from "./constants";
import ScoreBreakupDataGrid from "./score-breakup-datagrid";
import Loader from "../loader/loader";
import CircleIcon from "@mui/icons-material/Circle";
import { MatchScoreAvailabilitySxProps } from "./availability-view/constants";
import MarketPlaceIcon from "../../common/images/marketplace.png";
import { routeToEmployeeProfile } from "../../global/utils";

const SystemSuggestionCardView = (props: IPropsCardView) => {
  const updateSelections = (user: ISystemSuggestions, checked: boolean) => {
    let tempSelections = props.suggestionsSelected;
    if (checked) {
      tempSelections = [...tempSelections, user];
    } else {
      tempSelections = tempSelections.filter(
        (userItem: ISystemSuggestions) => userItem.email !== user.email
      );
    }
    props.updateSelections(tempSelections);
  };

  const handleScroll = () => {
    if (
      window.innerHeight + window.scrollY >= document.body.offsetHeight - 200 &&
      !props.isLoading
    ) {
      fetchNewSuggestionsListener();
    }
  };

  const debounceFetchSuggestion = (fn: any, delay: number) => {
    let timeout: any;
    return (...args: any) => {
      clearTimeout(timeout);
      timeout = setTimeout(() => {
        fn(...args);
      }, delay);
    };
  };

  useEffect(() => {
    const debounceScroll = debounceFetchSuggestion(handleScroll, 300);
    window.addEventListener("scroll", debounceScroll);
    return () => {
      window.removeEventListener("scroll", debounceScroll);
    };
  }, [props.userSuggestions]);

  const fetchNewSuggestionsListener = async () => {
    if (props.isLoading || props.list_ended) return;

    props.setIsLoading(true);

    const newConfig = {
      ...props.fetchDetailsConfig,
      pagination: props.fetchDetailsConfig.pagination + 1,
    };
    props.setFetchDetailsConfig(newConfig);
    await Promise.all([
      props.fetchSystemSuggestions(
        props.requisitionId,
        props.fetchDetailsConfig.pagination + 1
      ),
    ]);
    props.setIsLoading(false);
  };
  const CalculateTotalScore = (score_breakup: IScoreBreakup[]) => {
    let totalScore: number = 0;
    if (Array.isArray(score_breakup)) {
      score_breakup?.forEach((breakup) => {
        if (breakup && breakup?.value) {
          totalScore += parseInt(breakup.value.toString());
        }
      });
    }
    return totalScore;
  };

  return (
    <Typography component={"div"}>
      <Grid container spacing={3}>
        {props.userSuggestions.map(
          (item: ISystemSuggestions, index: number) => {
            const isUserAlreadySelected = props.suggestionsSelected.find(
              (userSelected) =>
                userSelected.email.toLowerCase().trim() ===
                item.email.toLowerCase().trim()
            );

            const isDisabled = !item.available ;
            return (
              <Grid
                item
                xs={2}
                sm={6}
                md={4}
                lg={3}
                key={index}
                className="card-main-container"
              >
                <Card raised>
                  <div>
                    <CardContent sx={{ m: 0.5, backgroundColor: "white" }}>
                      <Typography
                        sx={{ fontSize: 14, backgroundColor: "white" }}
                        color="text.secondary"
                        gutterBottom
                        component={"div"}
                      >
                        <Grid container spacing={2}>
                          <Grid item xs={1} sx={UserDetailsHeaderSxProps}>
                            <Checkbox
                              checked={
                                props.suggestionsSelected.find(
                                  (selections) =>
                                    selections.email === item.email
                                )
                                  ? true
                                  : false
                              }
                              onChange={(e) =>
                                updateSelections(item, e.target.checked)
                              }
                              disabled={isDisabled}
                            />
                          </Grid>
                          <Grid
                            item
                            xs={8}
                            sx={UserDetailsHeaderSxProps}
                            className="card-empName-header"
                          >
                            <CircleIcon
                              sx={{
                                color: !item.available ? "red" : "green",
                                fontSize: "1rem",
                                marginLeft: "10px",
                                marginRight: "10px",
                              }}
                            />
                            <span
                              style={{ cursor: "pointer", color: "blue" }}
                              onClick={() =>
                                routeToEmployeeProfile(
                                  `/employee-profile/${item.email}`
                                )
                              }
                            >
                              {item.empName}
                            </span>
                            &nbsp;
                            <span className="card-empName-header">
                              {`(Total-${CalculateTotalScore(
                                item.score_breakup
                              )})`}
                            </span>
                          </Grid>

                          <Grid item xs={1} sx={UserDetailsHeaderSxProps}>
                            {item.interested ? (
                              <img
                                src={MarketPlaceIcon}
                                alt="upload"
                                style={{
                                  height: "22px",
                                  width: "22px",
                                }}
                              />
                            ) : (
                              <></>
                            )}
                          </Grid>
                          <Grid item xs={2}>
                            <Typography
                              component={"div"}
                              sx={MatchScoreAvailabilitySxProps}
                            >
                              {item.score}%
                            </Typography>
                          </Grid>
                        </Grid>
                      </Typography>
                      <Typography
                        gutterBottom
                        component={"div"}
                        sx={{ backgroundColor: "white" }}
                      >
                        <Box sx={{ padding: "16px 0px 16px 10px" }}>
                          <ScoreBreakupDataGrid
                            sx={DataGridSxProps}
                            data={item}
                            categories={props.requisitionDetails.requisitionParameters
                              .filter(
                                (m) => m?.requisitionWeight > 0 && m.isChecked
                              )
                              .map((column: any) => column.category)}
                          />
                        </Box>
                      </Typography>
                    </CardContent>
                  </div>
                </Card>
              </Grid>
            );
          }
        )}
      </Grid>
      <Grid container spacing={2} sx={{ m: 1 }}>
        <Grid item xs={5.5} />
        <Grid item xs={1}>
          {props.list_ended ? (
            <>No Suggestions</>
          ) : (
            <>{props?.isLoading ? <Loader small={true} /> : <></>}</>
          )}
        </Grid>
        <Grid item xs={5.5} />
      </Grid>
    </Typography>
  );
};
export default memo(SystemSuggestionCardView);
