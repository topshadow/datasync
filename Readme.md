mkdir -p $HOME/dotnet && tar zxf dotnet-sdk-8.0.100-preview.3.23178.7-linux-x64.tar.gz -C $HOME/dotnet
export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet
