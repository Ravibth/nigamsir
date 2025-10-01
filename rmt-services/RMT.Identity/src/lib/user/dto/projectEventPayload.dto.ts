export interface ProjectEventPayloadDto {
  action: string;
  token: string;
  payload: string;
  response_payload?: string;
}
