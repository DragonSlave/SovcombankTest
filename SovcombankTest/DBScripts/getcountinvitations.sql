-- FUNCTION: public.getcountinvitations(integer)

-- DROP FUNCTION public.getcountinvitations(integer);

CREATE OR REPLACE FUNCTION public.getcountinvitations(
	apiid integer DEFAULT 4)
    RETURNS integer
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
AS $BODY$DECLARE 
invitations_count integer;
   
BEGIN

SELECT COUNT(*) INTO invitations_count FROM public."Invitation" WHERE createdon = CURRENT_DATE;
return invitations_count;

END;
$BODY$;

ALTER FUNCTION public.getcountinvitations(integer)
    OWNER TO postgres;
