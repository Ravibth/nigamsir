import { Controller } from "@nestjs/common";

import { workflowHistoryService } from "./workflowHistory.service";

@Controller("history")
export class WorkflowHistoryController {
  constructor(private service: workflowHistoryService) {}
}
