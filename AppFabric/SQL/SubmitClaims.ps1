foreach( $Arg in $Args )
{
	invoke-sqlcmd -query "exec SubmitClaim $Arg" -database ContosoDB
}
