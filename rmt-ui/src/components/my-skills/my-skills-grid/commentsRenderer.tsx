import { useContext, useEffect, useState } from "react";
import {
  IGetAllMySkillsResponse,
  IGetWorkflowCommentsByItemIdResponse,
  IGetWorkflowCommentsByItemIdResponseTaskList,
  getWorkflowCommentsByItemId,
} from "../../../services/skills/userSkills.service";
import Loader from "../../loader/loader";
import {
  SnackbarContext,
  SnackbarContextProps,
  SnackbarSeverity,
} from "../../../contexts/snackbarContext";
import { List, ListItem, ListItemText, Typography } from "@mui/material";
import { commentsHoverSxProps, ListCommentsHoverSxProps } from "./utils";
import moment from "moment";
import { getDateInMomentFormat } from "../../../utils/date/dateHelper";
import {
  UserDetailsContext,
  IUserDetailsContext,
} from "../../../contexts/userDetailsContext";
import { IUserSkillsWorkflowCommentDTO } from "../../skill-review/utils";
import { getEmailId } from "../../../global/utils";

export interface IMySkillCommentRendererProps {
  nodeData: IGetAllMySkillsResponse;
  gridRowsData: IGetAllMySkillsResponse[];
  setGridRowsData: React.Dispatch<
    React.SetStateAction<IGetAllMySkillsResponse[]>
  >;
}

const MySkillCommentRenderer = (props: IMySkillCommentRendererProps) => {
  const [openLoader, setOpenLoader] = useState<boolean>(false);
  const snackbarContext: SnackbarContextProps = useContext(SnackbarContext);
  const userDetailsContext: IUserDetailsContext =
    useContext(UserDetailsContext);

  const updateCommentsInGridRowData = (
    resp: IGetWorkflowCommentsByItemIdResponse[]
  ) => {
    const tempRows: IGetAllMySkillsResponse[] = props.gridRowsData.map(
      (row) => {
        if (row.id === props.nodeData.id) {
          const comments = resp.find(
            (item) => item.item_id === props.nodeData.id
          );
          if (comments) {
            return {
              ...row,
              commentsFetched: true,
              comments: comments.task_list,
            };
          }
          return { ...row, commentsFetched: true, comments: [] };
        } else {
          return row;
        }
      }
    );
    props.setGridRowsData(tempRows);
  };

  const getComments = (): Promise<boolean> => {
    return new Promise<boolean>((resolve, reject) => {
      if (props.nodeData?.commentsFetched) {
        resolve(true);
      } else {
        getWorkflowCommentsByItemId([props?.nodeData?.id])
          .then((resp: IGetWorkflowCommentsByItemIdResponse[]) => {
            updateCommentsInGridRowData(resp);
            resolve(false);
          })
          .catch((err) => {
            reject(err);
          });
      }
    });
  };

  const GetComments = () => {
    setOpenLoader(true);
    Promise.all([getComments()])
      .then(() => {
        setOpenLoader(false);
      })
      .catch(() => {
        snackbarContext.displaySnackbar(
          "Error fetching comments",
          SnackbarSeverity.ERROR
        );
        setOpenLoader(false);
      });
  };

  useEffect(() => {
    GetComments();
  }, []);

  const getUserSkillWorkflowCommentRenderer = (
    allComments: IGetWorkflowCommentsByItemIdResponseTaskList[]
  ) => {
    const commentsParsed: IUserSkillsWorkflowCommentDTO[] = [];
    allComments.forEach((item) => {
      commentsParsed.push(...JSON.parse(item.comment));
    });

    return commentsParsed
      .sort((a, b) =>
        moment(new Date(a.created_at)).isBefore(new Date(b.created_at)) ? 1 : -1
      )
      .map((value, index) => (
        <ListItem key={index}>
          <ListItemText
            primary={
              <>
                <span style={{ fontStyle: "italic" }}>
                  {getDateInMomentFormat(value.created_at, "DD-MM-YYYY")}(
                  {value.name ? value.name : getEmailId(value.created_by)}) -{" "}
                </span>
                <span>{value.comment}</span>
              </>
            }
          />
        </ListItem>
      ));
  };

  return (
    <Typography component={"div"} sx={commentsHoverSxProps}>
      {openLoader ? (
        <Loader small={true} />
      ) : (
        <>
          {props.nodeData?.commentsFetched && props.nodeData.comments.length ? (
            <>
              Comments:
              <List sx={ListCommentsHoverSxProps}>
                {getUserSkillWorkflowCommentRenderer(props.nodeData.comments)}
              </List>
            </>
          ) : (
            <>No Comments</>
          )}
        </>
      )}
    </Typography>
  );
};
export default MySkillCommentRenderer;
