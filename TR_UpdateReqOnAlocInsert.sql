CREATE TRIGGER TR_UpdateReqOnAlocInsert
AFTER INSERT ON "PublishedResAllocDetails"
FOR EACH ROW
EXECUTE FUNCTION FN_UpdateReqOnAlocUpdate();