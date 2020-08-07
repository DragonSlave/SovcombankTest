-- FUNCTION: public.approve_user(text)

-- DROP FUNCTION public.approve_user(text);

CREATE OR REPLACE FUNCTION public.approve_user(
	userphone text)
    RETURNS integer
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
AS $BODY$DECLARE  
invitations_count integer;
user_saved integer;
user_approved integer;

BEGIN

SELECt COUNT(*) INTO invitations_count FROM public."Invitation" WHERE phone = userphone;

SELECt COUNT(*) INTO user_saved FROM public."User" WHERE phone = userphone;

IF (user_saved > 0) THEN
	SELECT 0  INTO user_approved;
END IF;

IF (invitations_count > 0) THEN
	INSERT INTO public."User" 
	(phone)
	SELECT userphone;
	SELECT 1 INTO user_approved;
END IF;

return user_approved;

END;
$BODY$;

ALTER FUNCTION public.approve_user(text)
    OWNER TO postgres;
