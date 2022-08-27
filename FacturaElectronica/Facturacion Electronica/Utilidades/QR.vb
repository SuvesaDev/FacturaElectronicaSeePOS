Imports ThoughtWorks.QRCode.Codec.QRCodeEncoder
Imports System.Drawing.Image
Imports System.Windows.Forms

Namespace OBSoluciones
    Namespace Utilidades
        Public Class QR

            Public Sub GenerarCodigo(ByVal _Archivo As String, ByVal _Texto As String)
                Dim qrCodeEncoder As ThoughtWorks.QRCode.Codec.QRCodeEncoder = New ThoughtWorks.QRCode.Codec.QRCodeEncoder()
                Dim encoding As String = "Byte"

                If encoding = "Byte" Then
                    qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE
                ElseIf encoding = "AlphaNumeric" Then
                    qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC
                ElseIf encoding = "Numeric" Then
                    qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.NUMERIC
                End If

                Try
                    Dim scale As Integer = Convert.ToInt16(4)
                    qrCodeEncoder.QRCodeScale = scale
                Catch ex As Exception
                    MessageBox.Show("Invalid size!")
                    Return
                End Try

                Try
                    Dim version As Integer = Convert.ToInt16(7)
                    qrCodeEncoder.QRCodeVersion = version
                Catch ex As Exception
                    MessageBox.Show("Invalid version !")
                End Try

                Dim errorCorrect As String = "M"

                If errorCorrect = "L" Then
                    qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.L
                ElseIf errorCorrect = "M" Then
                    qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M
                ElseIf errorCorrect = "Q" Then
                    qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.Q
                ElseIf errorCorrect = "H" Then
                    qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.H
                End If

                Dim image As Drawing.Image
                image = qrCodeEncoder.Encode(_Texto)
                image.Save(_Archivo)
            End Sub
        End Class
    End Namespace
End Namespace
