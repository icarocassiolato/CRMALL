program ClientAPP;

uses
  System.StartUpCopy,
  FMX.Forms,
  uClientAPP in 'uClientAPP.pas' {frmTesteCRMALL};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmTesteCRMALL, frmTesteCRMALL);
  Application.Run;
end.
