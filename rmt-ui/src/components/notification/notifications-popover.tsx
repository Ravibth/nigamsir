import React, {
  useCallback,
  useContext,
  useEffect,
  useRef,
  useState,
} from "react";
import {
  INotificationContext,
  NotificationContext,
  NotificationSocketMethods,
} from "../../contexts/notificationContext";
import {
  IUserDetailsContext,
  UserDetailsContext,
} from "../../contexts/userDetailsContext";
import {
  ENotificationsFilter,
  IPushNotifications,
  MarkAllNotificationsAsReadByApiService,
  MarkNotificationsAsReadByApiService,
  NotificationSxProps,
  ZeroNotificationSxProps,
  getNotificationsByApiService,
} from "./constant";
import ExtensionNotification from "./ProjectExtendNotification/notificationExtend";
import AllocationNotification from "./ProjectAllocaionNotification/NotificationAllocation";
import { Button, Chip, Grid, Tooltip, Typography } from "@mui/material";
import MarkChatReadOutlinedIcon from "@mui/icons-material/MarkChatReadOutlined";
import Loader from "../loader/loader";
import "./notifications.css";
import { useNavigate } from "react-router-dom";
import {
  LoaderContext,
  LoaderContextProps,
} from "../../contexts/loaderContext";

const NotificationsPopOver = (props: any) => {
  const navigate = useNavigate();
  const notificationSocket: INotificationContext =
    useContext(NotificationContext);
  const userDetailsContext: IUserDetailsContext =
    useContext(UserDetailsContext);
  const [notifications, setNotifications] = useState<IPushNotifications[]>([]);
  const [currentNotifications, setCurrentNotifications] = useState<
    IPushNotifications[]
  >([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const fetchDetailsConfigLimit = 20;
  let pagination = 1;
  const [listEnded, setListEnded] = useState<boolean>(false);
  const componentRef = useRef(null);
  const [hovering, setHovering] = useState(false);
  const [showAllNotifications, setShowAllNotifications] =
    useState<boolean>(true);
  const loaderContext: LoaderContextProps = useContext(LoaderContext);

  useEffect(() => {
    const element: any = componentRef.current;
    const debounceScroll = debounceFetchNotifications(handleScroll, 300);

    if (element) {
      // Add a scroll event listener to the specific component
      element.addEventListener("scroll", debounceScroll);

      // Remove the event listener when the component is unmounted
      return () => {
        element.removeEventListener("scroll", debounceScroll);
      };
    }
  }, []);

  const fetchNewNotificationsListener = async () => {
    if (isLoading || listEnded) return;

    const runNotifications =
      process.env.REACT_APP_RUN_NOTIFICATION + "" === "true";
    const runNotificationsBySocket =
      process.env.REACT_APP_PUSH_NOTIFICATION_BY_TIMEINTERVAL + "" === "false";

    if (runNotifications && runNotificationsBySocket) {
      await Promise.all([getNotifications()]);
    }
    const runNotificationsByTimeInterval =
      process.env.REACT_APP_PUSH_NOTIFICATION_BY_TIMEINTERVAL + "" === "true";
    if (runNotifications && runNotificationsByTimeInterval && !listEnded) {
      await Promise.all([getNotificationsByApi()]);
    }
  };

  const debounceFetchNotifications = (fn: any, delay: number) => {
    let timeout: any;
    return (...args: any) => {
      clearTimeout(timeout);
      timeout = setTimeout(() => {
        fn(...args);
      }, delay);
    };
  };

  const handleScroll = () => {
    const element: any = componentRef.current;
    const threshold = 5; // Adjust this value as needed
    if (
      element.scrollHeight - element.scrollTop <=
        element.clientHeight + threshold &&
      !isLoading
    ) {
      fetchNewNotificationsListener();
    }
  };

  const getNotifications = async () => {
    setIsLoading(true);
    notificationSocket.notificationSocketConnection?.send(
      NotificationSocketMethods.GetLoggedInUserNotifications,
      {
        email: userDetailsContext.username,
        limit: fetchDetailsConfigLimit,
        pagination: pagination,
      }
    );
    pagination = pagination + 1;
    setIsLoading(false);
  };

  const updateNotifications = (
    newlyAdded: IPushNotifications[],
    newNotification: boolean = false
  ) => {
    let tempNotifications = notifications;
    newlyAdded?.forEach((element) => {
      if (!tempNotifications.find((item: any) => item.id === element.id)) {
        newNotification
          ? tempNotifications.unshift(element)
          : tempNotifications.push(element);
      }
    });
    tempNotifications = tempNotifications.sort((a, b) =>
      new Date(a.createdDate) > new Date(b.createdDate) ? 0 : 1
    );
    setNotifications(tempNotifications);
  };

  useEffect(() => {
    filterNotifications(
      showAllNotifications
        ? ENotificationsFilter.ALL
        : ENotificationsFilter.UNREAD
    );
  }, [notifications]);

  useEffect(() => {
    const runNotifications =
      process.env.REACT_APP_RUN_NOTIFICATION + "" === "true";
    const runNotificationsBySocket =
      process.env.REACT_APP_PUSH_NOTIFICATION_BY_TIMEINTERVAL + "" === "false";
    if (runNotifications && runNotificationsBySocket) {
      if (notificationSocket.isConnected) {
        getNotifications();
        notificationSocket.notificationSocketConnection?.on(
          NotificationSocketMethods.GetLoggedInUserAllNotifications,
          (resp) => {
            if (resp.length < fetchDetailsConfigLimit) {
              setListEnded(true);
            }
            updateNotifications(resp);
            setIsLoading(false);
          }
        );
        notificationSocket.notificationSocketConnection?.on(
          NotificationSocketMethods.FoundNewNotification,
          (resp) => {
            updateNotifications([resp], true);
            setIsLoading(false);
          }
        );
      }
    }
    return () => {
      const runNotifications =
        process.env.REACT_APP_RUN_NOTIFICATION + "" === "true";
      const runNotificationsBySocket =
        process.env.REACT_APP_PUSH_NOTIFICATION_BY_TIMEINTERVAL + "" ===
        "false";
      if (runNotifications && runNotificationsBySocket) {
        notificationSocket.notificationSocketConnection?.off(
          NotificationSocketMethods.GetLoggedInUserNotifications
        );
        notificationSocket.notificationSocketConnection?.off(
          NotificationSocketMethods.GetLoggedInUserAllNotifications
        );
      }
    };
  }, [notificationSocket.isConnected]);

  useEffect(() => {
    const runNotifications =
      process.env.REACT_APP_RUN_NOTIFICATION + "" === "true";
    const runNotificationsByTimeInterval =
      process.env.REACT_APP_PUSH_NOTIFICATION_BY_TIMEINTERVAL + "" === "true";
    if (runNotifications && runNotificationsByTimeInterval) {
      getNotificationsByApi();
    }
  }, []);

  const getNotificationsByApi = () => {
    return new Promise<boolean>((resolve, reject) => {
      setIsLoading(true);
      getNotificationsByApiService(fetchDetailsConfigLimit, pagination)
        .then((resp) => {
          if (resp.length < fetchDetailsConfigLimit) {
            setListEnded(true);
          }
          updateNotifications(resp);
          pagination = pagination + 1;
          setIsLoading(false);
          resolve(true);
        })
        .catch((err) => {
          setIsLoading(false);
          resolve(true);
        });
    });
  };

  const handleNotificationClick = async (item: IPushNotifications) => {
    const runNotifications =
      process.env.REACT_APP_RUN_NOTIFICATION + "" === "true";
    const runNotificationsByTimeInterval =
      process.env.REACT_APP_PUSH_NOTIFICATION_BY_TIMEINTERVAL + "" === "true";

    if (runNotifications && !runNotificationsByTimeInterval) {
      notificationSocket.notificationSocketConnection?.send(
        NotificationSocketMethods.MarkNotificationsAsRead,
        [item.id]
      );
    }

    if (runNotifications && runNotificationsByTimeInterval) {
      loaderContext.open(true);
      await MarkNotificationsAsReadByApi([item.id]);
      loaderContext.open(false);
    }
    if (item?.link) {
      const url = process.env?.REACT_APP_REDIRECT_URI + item?.link;
      window.location.replace(url);
      //navigate(item?.link, { replace: true });
    }
    props.handleClose();
  };

  const MarkNotificationsAsReadByApi = (id: string[]): Promise<boolean> => {
    return new Promise<boolean>((resolve, reject) => {
      MarkNotificationsAsReadByApiService(id)
        .then(() => {
          resolve(true);
        })
        .catch((e) => {
          resolve(true);
        });
    });
  };

  const MarkAllNotificationsAsReadByApi = (): Promise<boolean> => {
    return new Promise<boolean>((resolve, reject) => {
      MarkAllNotificationsAsReadByApiService()
        .then(() => {
          resolve(true);
        })
        .catch((e) => {
          resolve(true);
        });
    });
  };

  const handleMouseOver = useCallback(() => {
    setHovering(true);
  }, []);
  const handleMouseOut = useCallback(() => {
    setHovering(false);
  }, []);

  const filterNotifications = (type: string) => {
    let tempNotifications = [];
    switch (type) {
      case ENotificationsFilter.ALL:
        // setShowAllNotifications(true);
        tempNotifications = notifications;
        break;
      case ENotificationsFilter.UNREAD:
        tempNotifications = notifications.filter((item) => !item.isRead);
        // setShowAllNotifications(false);
        break;
      default:
        return;
    }
    setCurrentNotifications([]);
    setCurrentNotifications(tempNotifications);
  };

  const readAllBtnClicked = async () => {
    await MarkAllNotificationsAsReadByApi();
    props.getMyNotificationsCountByApi();
    props.handleClose();
  };

  return (
    <Grid
      container
      className="notification-popover p-b-10"
      style={{
        overflowY: hovering ? "auto" : "hidden",
      }}
      onMouseOver={handleMouseOver}
      onMouseOut={handleMouseOut}
      ref={componentRef}
    >
      <Grid
        item
        xs={12}
        className="notification-header"
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        <span>Notifications</span>
        <Tooltip title="Mark all as read">
          <Button
            onClick={readAllBtnClicked}
            size="small"
            sx={{
              mr: "15px",
              minWidth: 0,
              width: 40,
              height: 40,
              borderRadius: "50%",
              border: "none",
              backgroundColor: "#00000014",
              color: "#000000de",
              "&:hover": {
                backgroundColor: "#00000030",
                color: "#000000",
              },
            }}
          >
            <MarkChatReadOutlinedIcon />
          </Button>
        </Tooltip>
      </Grid>
      <Grid item xs={1.6}>
        <Chip
          label={ENotificationsFilter.ALL}
          className={showAllNotifications ? "showAllNotifications" : ""}
          onClick={() => {
            setShowAllNotifications(true);
            filterNotifications(ENotificationsFilter.ALL);
          }}
        />
      </Grid>
      <Grid item xs={4} className="m-b-10">
        <Chip
          className={!showAllNotifications ? "showAllNotifications" : ""}
          label={ENotificationsFilter.UNREAD}
          onClick={() => {
            setShowAllNotifications(false);
            filterNotifications(ENotificationsFilter.UNREAD);
          }}
        />
      </Grid>
      {currentNotifications && currentNotifications.length > 0 ? (
        <>
          {currentNotifications.map((notification, index) => {
            const Component =
              notification.type === "extension"
                ? ExtensionNotification
                : AllocationNotification;

            return (
              <Typography
                key={index}
                sx={{
                  ...NotificationSxProps,
                  backgroundColor: notification.isRead ? "white" : "#f5eff9",
                }}
                component={"div"}
                className="notification-item"
                onClick={() => handleNotificationClick(notification)}
              >
                <Component
                  receivedAt={notification.createdDate}
                  content={notification.message}
                  isRead={notification.isRead}
                />
              </Typography>
            );
          })}
        </>
      ) : (
        <Grid item xs={12}>
          <Typography sx={ZeroNotificationSxProps}>
            {!isLoading && <>No New Notifications</>}
          </Typography>
        </Grid>
      )}
      {isLoading ? (
        <Grid item xs={12}>
          <Typography sx={ZeroNotificationSxProps}>
            <Loader small={true} />
          </Typography>
        </Grid>
      ) : (
        <></>
      )}
    </Grid>
  );
};
export default NotificationsPopOver;
