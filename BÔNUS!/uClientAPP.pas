unit uClientAPP;

interface

uses
  System.SysUtils, System.Types, System.UITypes, System.Classes, System.Variants,
  FMX.Types, FMX.Controls, FMX.Forms, FMX.Graphics, FMX.Dialogs, REST.Types,
  System.Rtti, FMX.Grid.Style, FMX.ScrollBox, FMX.Grid, FMX.StdCtrls,
  FMX.Controls.Presentation, FMX.Edit, Data.Bind.Components,
  Data.Bind.ObjectScope, REST.Client, IdBaseComponent, IdComponent,
  IdTCPConnection, IdTCPClient, IdHTTP, JSON, IdCoder, IdCoder3to4, IdCoderMIME,
  FMX.Memo, Data.Bind.EngExt, Fmx.Bind.DBEngExt, Fmx.Bind.Grid,
  System.Bindings.Outputs, Fmx.Bind.Editors, Data.Bind.Grid, FireDAC.Stan.Intf,
  FireDAC.Stan.Option, FireDAC.Stan.Param, FireDAC.Stan.Error, FireDAC.DatS,
  FireDAC.Phys.Intf, FireDAC.DApt.Intf, Data.DB, FireDAC.Comp.DataSet,
  FireDAC.Comp.Client, Data.Bind.DBScope, REST.Response.Adapter;

type
  TfrmTesteCRMALL = class(TForm)
    edtHost: TEdit;
    btnListarTodos: TButton;
    RESTClient: TRESTClient;
    RESTRequest: TRESTRequest;
    RESTResponse: TRESTResponse;
    BindingsList: TBindingsList;
    sgGrid: TStringGrid;
    FDMemTable: TFDMemTable;
    BindSourceDB: TBindSourceDB;
    LinkGridToDataSourceBindSourceDB1: TLinkGridToDataSource;
    RESTResponseDataSetAdapter: TRESTResponseDataSetAdapter;
    btnDeletar: TButton;
    edtPesquisa: TEdit;
    lblPesquisa: TLabel;
    procedure btnListarTodosClick(Sender: TObject);
    procedure edtPesquisaExit(Sender: TObject);
    procedure btnDeletarClick(Sender: TObject);
  private
    procedure FazerRequisicao(psRequisicao: string = ''; prrmTipoRequisicao: TRESTRequestMethod = rmGET);
    procedure FazerPesquisa;
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmTesteCRMALL: TfrmTesteCRMALL;

implementation

uses
  StrUtils;

{$R *.fmx}

procedure TfrmTesteCRMALL.FazerPesquisa;
var
  sRequisicao: string;
begin
  sRequisicao := '?ID=' + edtPesquisa.Text;
  FazerRequisicao(sRequisicao);
end;

procedure TfrmTesteCRMALL.edtPesquisaExit(Sender: TObject);
begin
  FazerPesquisa;
end;

procedure TfrmTesteCRMALL.FazerRequisicao(psRequisicao: string = ''; prrmTipoRequisicao: TRESTRequestMethod = rmGET);
begin
  RESTClient.BaseURL := edtHost.Text + psRequisicao;
  RESTRequest.Method := prrmTipoRequisicao;
  RESTRequest.Client := RESTClient;
  RESTRequest.Response := RESTResponse;
  RESTRequest.Execute;
end;


procedure TfrmTesteCRMALL.btnDeletarClick(Sender: TObject);
var
  sIDSelecionado: string;
begin
  sIDSelecionado := sgGrid.Cells[0, sgGrid.Selected];

  if sIDSelecionado = EmptyStr then
  begin
    ShowMessage('Nenhum registro selecionado');
    Exit;
  end;

  FazerRequisicao('?ID=' + sIDSelecionado, rmDELETE);
end;

procedure TfrmTesteCRMALL.btnListarTodosClick(Sender: TObject);
begin
  FazerRequisicao;
end;

end.
