SCRIPT_DIR=$( cd "$( dirname "$0" )" && pwd )
MAIN="$SCRIPT_DIR/bin/Mao_Good_Transite_x64d.dll"
export LD_LIBRARY_PATH="$SCRIPT_DIR/bin:$LD_LIBRARY_PATH"
if [ -f "$MAIN" ]; then
	dotnet "$MAIN" 
else
	echo "Application executable not found"
fi
